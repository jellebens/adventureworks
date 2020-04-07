using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Adventureworks.Document.Controllers
{
    [Route("api/system")]
    [Route("api/public/documents/system")]
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public override ActionResult IsLive()
        {
            return Ok();
        }

        [HttpGet]
        [Route("ready")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public override ActionResult IsReady()
        {
            try
            {
                string connectionString = Environment.GetEnvironmentVariable("DOCUMENT_BUS");

                QueueClient queueClient = new QueueClient(connectionString, "docsin");
            }
            catch (Exception exc)
            {
                logger.LogCritical("Failed to connect to Queue {0}", exc.Message);
                return StatusCode(500);
            }


            return Ok();
        }
    }
}