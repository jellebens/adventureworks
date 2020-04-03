using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Adventureworks.Document.Data;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace Adventureworks.Document.Controllers
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
        public async void Upload(QueueFile file)
        {
            _Logger.LogInformation("Queueing: " + file.FileName);

            string connectionString = Environment.GetEnvironmentVariable("DOCUMENT_STORAGE");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("docsin");

            await queue.CreateIfNotExistsAsync();


            await queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(file)));

            _Logger.LogInformation("Queued file " + file.FileName);
        }
    }
}