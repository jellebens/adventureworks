using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Adventureworks.Core.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data;

namespace Adventureworks.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : SystemControllerBase
    {

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
            bool isReady = false;


            using (PurchasingDbContext ctx = new PurchasingDbContext())
            {
                isReady = true;
            }

            if (isReady)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}