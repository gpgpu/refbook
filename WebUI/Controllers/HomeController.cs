using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

using Domain.Portals;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ViewResult Index()
        {
            


            return View();
            
        }

        [HttpPost]
        public ActionResult SignIn(string userName, string password)
        {
            if (userName == "f" && password == "f")
                return RedirectToAction("Index", "Portal");

            TempData["message"] = "wrong user name or password";
            return View();

        }

    }
}
