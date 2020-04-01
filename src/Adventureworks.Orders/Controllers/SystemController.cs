using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrderService.Data;

namespace Adventureworks.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : SystemControllerBase
    {
        private readonly PurchasingDbContext purchasingDbContext;
        private readonly ILogger<SystemController> _Logger;

        public SystemController(PurchasingDbContext purchasingDbContext, ILogger<SystemController> logger)
        {
            this.purchasingDbContext = purchasingDbContext;
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
            bool isReady = purchasingDbContext.Database.CanConnect();

            if (isReady)
            {
                return Ok();
            }
            else
            {
                _Logger.LogWarning("Failed to connect to database");
                return StatusCode(500);
            }
        }
    }
}