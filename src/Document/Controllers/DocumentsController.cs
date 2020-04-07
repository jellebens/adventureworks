using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Adventureworks.Document.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Adventureworks.Document.Controllers
{
    [ApiController]
    [Route("api/public/documents")]
    public class DocumentsController : Controller
    {
        private readonly ILogger<DocumentsController> _Logger;

        public DocumentsController(ILogger<DocumentsController> logger)
        {
            this._Logger = logger;
        }

        [HttpPost]
        [Route("upload")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(SerializableError), StatusCodes.Status400BadRequest)]
        public async void Upload(QueueFile file)
        {
            _Logger.LogInformation("Queueing: " + file.FileName);

            string connectionString = Environment.GetEnvironmentVariable("DOCUMENT_BUS");

            QueueClient queueClient = new QueueClient(connectionString, "docsin");
            try
            {
                byte[] payload = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(file));

                Message message = new Message(payload);

                await queueClient.SendAsync(message);

                _Logger.LogInformation("Queued file " + file.FileName);
            }
            catch (Exception exc)
            {
                _Logger.LogCritical("Failed to queue file " + file.FileName);
                throw exc;
            }
            finally {
                queueClient.CloseAsync().RunSynchronously();
            }
        }
    }
}