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
    public class PortalController : Controller
    {
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://local.refbookservice.org/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            var response = client.GetAsync("api/portal/Get").Result;
            List<Portal> portals = new List<Portal>();

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                portals = response.Content.ReadAsAsync<IEnumerable<Portal>>().Result.ToList();

            }
            ViewBag.status = response.StatusCode.ToString();
            return View(portals);
        }

    }
}
