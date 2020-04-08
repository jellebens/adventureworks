using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using DocumentProcessor.Data;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DocumentProcessor
{
    public class DocumentProcessor : BackgroundService
    {
        private IConfiguration _Configuration;
        private ILogger<DocumentProcessor> _Logger;
        
        public DocumentProcessor(IConfiguration configuration, ILogger<DocumentProcessor> logger)
        {
            _Logger = logger;
            _Configuration = configuration;
        }

        

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _Logger.LogInformation("Start Processing messages");

            string connectionString = _Configuration.GetValue<string>("DOCUMENT_BUS");

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 5,
                AutoComplete = true
            };
            QueueClient queueClient = new QueueClient(connectionString, "docsin");

            queueClient.RegisterMessageHandler(HandleMessage, messageHandlerOptions);

            

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            await queueClient.CloseAsync();
        }

        private async Task HandleMessage(Message message, CancellationToken token) {
            string body = Encoding.UTF8.GetString(message.Body);

            QueuedFile file = JsonConvert.DeserializeObject<QueuedFile>(body);
            _Logger.LogInformation($"Threating File: {file.Id} filename: {file.FileName}");
            string connectionString = _Configuration.GetValue<string>("DOCUMENT_STORAGE");

            BlobContainerClient containerClient = new BlobContainerClient(connectionString, "upload");
            containerClient.CreateIfNotExists();
            
            await containerClient.DeleteBlobAsync(file.Id.ToString());

            _Logger.LogInformation($"Threated File: {file.FileName}");
            
            
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _Logger.LogError($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            return Task.CompletedTask;
        }
    }
}
