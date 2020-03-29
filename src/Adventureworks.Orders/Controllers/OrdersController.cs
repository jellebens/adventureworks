using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adventureworks.Core.Contracts.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderService.Data;

namespace Adventureworks.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PurchasingDbContext _PurchasingDb;
        private readonly ILogger<OrdersController> _Logger;

        public OrdersController(PurchasingDbContext purchasingDb, ILogger<OrdersController> logger)
        {
            _PurchasingDb = purchasingDb;
            _Logger = logger;
        }
        [HttpGet]
        [Route("list/vendors")]
        public PurchaseOrdersByVendor[] PurchaseOrdersByVendor()
        {
            PurchaseOrdersByVendor[] vendors = _PurchasingDb.Vendors.Select(x => new PurchaseOrdersByVendor
            {
                Id = x.Id,
                Name = x.Name,
                NumOrders = x.Orders.Count(),

            }).OrderBy(o => o.Name).ToArray();



            return vendors;
        }

        [HttpGet]
        [Route("version")]
        public string Version() {
            string version = Environment.GetEnvironmentVariable("VERSION");
            _Logger.LogInformation($"Version: $(version)");
            if (string.IsNullOrEmpty(version))
            {
                version = "NOT SET";
            }
            return version;
        }
    }
}