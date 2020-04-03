using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Adventureworks.Document.Controllers
{
    [Route("api/system")]
    [ApiController]
    public class SystemController : SystemControllerBase
    {
        private readonly ILogger<SystemController> logger;

        public SystemController(ILogger<SystemController> logger)
        {
            this.logger = logger;
        }


        [HttpGet]
        [Route("live")]
        public override ActionResult IsLive()
        {
            return Ok();
        }

        [HttpGet]
        [Route("ready")]
        public override ActionResult IsReady()
        {
            string connectionString = Environment.GetEnvironmentVariable("DOCUMENT_STORAGE");
            try
            {
                BlobContainerClient containerClient = new BlobContainerClient(connectionString, "upload");
            }
            catch (Exception exc)
            {
                logger.LogCritical("Failed to connect to blob storage", exc.Message);
                return StatusCode(500);
            }


            return Ok();
        }
    }
}