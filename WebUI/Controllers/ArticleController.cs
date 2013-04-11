using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Domain.Articles;
using Domain.ExternalFiles;

using System.Data;
using System.Data.SqlClient;
using Infrastructure.Data;


namespace WebUI.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult EditContent(int id) // articleId
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://local.refbookservice.org/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string url = "api/article/get/" + id.ToString();

            var response = client.GetAsync(url).Result;
           

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var article = response.Content.ReadAsAsync<Article>().Result;
                return View(article);
            }

            return View();
          
        }

        [HttpPost]
        public void SaveContent(int articleId, string articleContent)
        {
            Infrastructure.Data.ArticleRepository rep = new Infrastructure.Data.ArticleRepository();

            rep.SaveContent(articleId, articleContent);
        }

        [HttpPost]
        public long AddArticle(Article newArticle)
        {
            Infrastructure.Data.ArticleRepository rep = new Infrastructure.Data.ArticleRepository();

            return rep.Create(newArticle);

        }

        [HttpPost]
        public string UploadFile(HttpPostedFileBase theFile)
        {
            if (theFile != null)
            {
            }
            else if (Request.Files.Count > 0)
            {
            }
            else if (Request.ContentLength == 0)
                throw new Exception("nothing uploaded");
            ExternalFile file = new ExternalFile();
            
            byte[] fileContent = new byte[Request.ContentLength];
            Request.InputStream.Read(fileContent, 0, Request.ContentLength);
            file.FileContent = fileContent;

            file.FileName = Request.Headers["X-File-Name"];
            file.MimeType = Request.Headers["X-File-Type"];
            file.ArticleId = long.Parse(Request.Headers["X-File-ArticleId"]);

            ExternalFileRepository rep = new ExternalFileRepository();
            var newId = rep.AddFile(file);
            return newId.ToString() ;
        }

        public FileContentResult GetImage(int imageId)
        {
            ExternalFileRepository rep = new ExternalFileRepository();

            ExternalFile file = rep.GetImage(imageId);

            return File(file.FileContent, file.MimeType);

        } 


    }
}
