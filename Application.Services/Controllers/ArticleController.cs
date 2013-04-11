using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Domain.Articles;

namespace Application.Services.Controllers
{
    public class ArticleController : ApiController
    {
        private IArticleRepository _articleRep;
        public ArticleController(IArticleRepository articleRep)
        {
            _articleRep = articleRep;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">articleId</param>
        /// <returns></returns>
        public Article Get(int id)
        {
            return _articleRep.GetArticleWithContent(id);
        }

        [HttpPost]
        public void Create(string articleName)
        {

        }
    }
}
