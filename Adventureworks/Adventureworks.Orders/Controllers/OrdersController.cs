using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adventureworks.Core.Contracts.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data;

namespace Adventureworks.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PurchasingDbContext _PurchasingDb;

        public OrdersController(PurchasingDbContext purchasingDb)
        {
            _PurchasingDb = purchasingDb;
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
    }
}