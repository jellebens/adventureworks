using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Adventureworks.Web.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public IActionResult Logout()
        {
            return View();
        }
    }
}