using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ling.Dashboard.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.SelectedMenu = "Dashboard";
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}