using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Adventureworks.Core.Mvc;
using Adventureworks.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Adventureworks.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : SystemControllerBase
    {
        private readonly ILogger _Logger;

        public SystemController(ILogger<SystemController> logger)
        {
            this._Logger = logger;
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
            return Ok();
        }
    }
}