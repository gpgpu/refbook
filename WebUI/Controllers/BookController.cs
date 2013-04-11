using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

using Domain.Books;
using Domain.Portals;
using ViewModels;

namespace WebUI.Controllers
{
    public class BookController : Controller
    {
        
        public ActionResult Manager([Bind(Prefix="parentId")]int portalId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://local.refbookservice.org/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string url = "api/book/" + portalId.ToString();

            var response = client.GetAsync(url).Result;
            List<Book> books = new List<Book>();

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var portal = response.Content.ReadAsAsync<Portal>().Result;
                return View(portal);
            }
            
            return View();
           
        }

        public ViewResult ngManager()
        {
            return View();
        }

        [HttpPost]
        public string SaveItemsOrder(List<ContentItem> items)
        {
            Infrastructure.Data.dapperBookRepository bookRep = new Infrastructure.Data.dapperBookRepository();

            bookRep.SaveItemsOrder(items);
            return "ok";
        }


    }
}
