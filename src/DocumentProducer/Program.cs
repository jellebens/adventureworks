using Azure.Storage.Blobs;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocumentProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DocumentProducer -generate <number to generate> <folder>");
            Console.WriteLine("DocumentProducer -upload <folder>");
            if (string.Equals("-generate", args[0])){
                int num;

                if (!int.TryParse(args[1], out num))
                {
                    num = 100;
                }

                string path = args[2];
                if (!Directory.Exists(path)) {
                    Console.WriteLine("Creating directory " + path);

                    Directory.CreateDirectory(path);
                }

                Parallel.For(0, num , x =>
                {
                    Console.WriteLine($"Generating document {x+1} of {num}");
                    File.Copy("samples/file-example_PDF_1MB.pdf", Path.Combine(args[2], $"{Guid.NewGuid()}.pdf"));
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Files Generated. Exiting.");
                return;
            }

            if (string.Equals("-upload", args[0]))
            {
                BlobContainerClient containerClient = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=jbens;AccountKey=w3atlmt+c4/9hAarSX6Zrme+bGsdxQGxF67QrWgwbZ9sOShtoBuN5tjMFttw75oelSKnxsB2JGyGN7Gih3fcYw==;EndpointSuffix=core.windows.net", "upload");
                containerClient.CreateIfNotExists();

                string path = args[1];
                string[] files = Directory.GetFiles(path);

                if (files.Length == 0) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("No Files Found. Exiting.");
                    return;
                }

                Parallel.ForEach(files, (file, state , x) =>
                {
                    Console.WriteLine($"Uploading document {x + 1} of {files.Length}");
                    Guid id = Guid.NewGuid();

                    containerClient.UploadBlob(id.ToString(), File.OpenRead(file));

                    HttpClient client = new HttpClient();

                    var payload = new {
                        Id = Guid.NewGuid(),
                        FileName = Path.GetFileName(file)
                    };

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                    var result = client.PostAsync("http://kubernetes.docker.internal/api/public/documents/upload", content).Result;

                    if (!result.IsSuccessStatusCode) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Failed to process document. Exiting. Error: " + result.ReasonPhrase);
                        containerClient.DeleteBlob(id.ToString());
                        return;
                    }

                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Files Generated. Exiting.");

            }
        }
    }
}
