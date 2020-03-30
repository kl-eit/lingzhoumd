using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ling.Dashboard.Controllers
{
    public class AccountController : Controller
    {

        #region Actions
        public ActionResult Login(String returnurl = "")
        {
            //if (Request.IsAuthenticated)
            //{
                //return RedirectToAction("Index", "Home");
            //}

            ViewBag.ReturnURL = returnurl;
            return View();
        }
        #endregion
    }
}