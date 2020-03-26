using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Adventureworks.Core.Service;
using Adventureworks.Web.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;

namespace  AdventureWorks.Web.Controllers
{
    public class HomeController : Controller
    {
        private TelemetryClient telemetry = new TelemetryClient();

        public IActionResult Index()
        {
            telemetry.TrackEvent("Home.Index");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
