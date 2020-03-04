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
    public class SystemController : PingableControllerBase
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
            _Logger.LogInformation("Pinging orderservice");
            var orderService = new Uri($"{Orders.System.Ready}");

            HttpClient client = new HttpClient();

            //HttpResponseMessage result = client.GetAsync(orderService).Result;

            //if (!result.IsSuccessStatusCode) {
            //    _Logger.LogWarning("Failed to call orderservice");
            //    throw new Exception();
            //}

            return Ok();
        }
    }
}