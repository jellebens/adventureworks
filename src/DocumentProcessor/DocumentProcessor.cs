using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentProcessor
{
    public class DocumentProcessor : BackgroundService
    {
        public DocumentProcessor(ILogger<DocumentProcessor> logger)
        {
            _Logger = logger;
        }

        public ILogger<DocumentProcessor> _Logger { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _Logger.LogInformation("Start Processing messages: ");

            string connectionString = Environment.GetEnvironmentVariable("DOCUMENT_STORAGE");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("docsin");

            await queue.CreateIfNotExistsAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
