using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace Document.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : SystemControllerBase
    {
        
        public SystemController()
        {
            
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
            string connectionString = Environment.GetEnvironmentVariable("documentStorage");


            BlobContainerClient containerClient = new BlobContainerClient(connectionString, "upload");

            return Ok();
        }
    }
}