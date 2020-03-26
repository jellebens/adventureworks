using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Adventureworks.Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Adventureworks.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger _Logger;
        

        public TestController(ILogger<TestController> logger)
        {
            _Logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Version()
        {
            _Logger.LogInformation("Getting Version");
            var orderService = new Uri($"{Orders.Version.Current}");

            HttpClient client = new HttpClient();

            var result = await client.GetAsync(orderService);

            if (!result.IsSuccessStatusCode)
            {
                _Logger.LogWarning("Failed to call orderservice");

                return Json("Failed to call service: " + result.StatusCode);
            }

            string response = await result.Content.ReadAsStringAsync();

            return Json(response);
        }
    }
}