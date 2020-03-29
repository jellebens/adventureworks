using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Adventureworks.Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Adventureworks.Web.Models;
using Newtonsoft.Json;

namespace Adventureworks.Web.Controllers
{
    public class VendorsController : Controller
    {
        private readonly ILogger _Logger;

        public VendorsController(ILogger<VendorsController> logger)
        {
            _Logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
           
        }

        [HttpGet]
        public async Task<JsonResult> OpenOrders() {
            _Logger.LogInformation("Getting Vendors");
            var orderService = new Uri($"{Orders.Vendors.List}");

            HttpClient client = new HttpClient();

            var result = await client.GetAsync(orderService);

            if (!result.IsSuccessStatusCode)
            {
                _Logger.LogWarning("Failed to call orderservice");

                return Json("");
            }
            
            string response = await result.Content.ReadAsStringAsync();

            VendorListItem[] vendors = JsonConvert.DeserializeObject<VendorListItem[]>(response); 

            return Json(vendors);
        }
    }
}