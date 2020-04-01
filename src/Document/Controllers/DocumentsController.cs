using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Document.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace Document.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController : Controller
    {
        private readonly ILogger<DocumentsController> _Logger;

        public DocumentsController(ILogger<DocumentsController> logger)
        {
            this._Logger = logger;
        }
        [Route("upload")]
        [HttpPost]
        public async void Upload(IFormFile file)
        {
            _Logger.LogInformation("Handling upload: " + file.FileName);

            string connectionString = Environment.GetEnvironmentVariable("DOCUMENT_STORAGE");


            BlobContainerClient containerClient = new BlobContainerClient(connectionString, "upload");
            containerClient.CreateIfNotExists();
            Guid id = Guid.NewGuid();

            await containerClient.UploadBlobAsync(id.ToString(), file.OpenReadStream());
            
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("docsin");

            await queue.CreateIfNotExistsAsync();

            var message = new
            {
                DocId = id,
                FileName = file.FileName
            };

            await queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(message)));

            _Logger.LogInformation("Queued file " + file.FileName);
        }
    }
}