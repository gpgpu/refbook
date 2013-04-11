using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Books;
using Domain.Portals;
using Domain.Topics;
using Domain.Articles;

namespace Application.Services.Controllers
{
    public class BookController : ApiController
    {
        private IPortalRepository _portalRep;
        private IBookRepository _bookRep;
        private ITopicRepository _topicRep;
        private IArticleRepository _articleRep;

        public BookController(IPortalRepository portalRep, IBookRepository bookRep, ITopicRepository topicRep, IArticleRepository articleRep)
        {
            _portalRep = portalRep;
            _bookRep = bookRep;
            _topicRep = topicRep;
            _articleRep = articleRep;
        }
        public Portal GetTOB(int id) // portalId
        {
            Domain.Books.BookService bookService = new BookService(_portalRep, _bookRep, _topicRep, _articleRep);
            return bookService.GetTableOfContent(id);
        }
    }
}
