using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Topics;
using Domain.Articles;

namespace Domain.Books
{
    public class BookService
    {
        private Domain.Portals.IPortalRepository _portalRep;
        private Domain.Books.IBookRepository _bookRep;
        private Domain.Topics.ITopicRepository _topicRep;
        private Domain.Articles.IArticleRepository _articleRep;

        public BookService(
            Domain.Portals.IPortalRepository portalRep,
            Domain.Books.IBookRepository bookRep,
            Domain.Topics.ITopicRepository topicRep,
            Domain.Articles.IArticleRepository articleRep
            )
        {
            this._portalRep = portalRep;
            this._bookRep = bookRep;
            this._topicRep = topicRep;
            this._articleRep = articleRep;
        }

        public Domain.Portals.Portal GetTableOfContent(int portalId)
        {
            var books = _bookRep.GetBooks(portalId).ToList();
            var topics = _topicRep.GetAllTopics(portalId).ToList();

            #region build dictionary
            // build a dictionary
            // O(n)
            Dictionary<int, Topic> dic = new Dictionary<int, Topic>();
            int previousBookId = -1;
            Book currentBook = null;
            for (int i = 0; i < topics.Count; i++)
            {
                var topic = topics[i];
                int currentBookId = topic.BookId;

                // start point of a topics block for another book
                if (previousBookId != currentBookId)
                {
                    currentBook = books.Where( b => b.Id==currentBookId).FirstOrDefault();
                    if (currentBook.Topics == null)
                    {
                        currentBook.Topics = new List<Topic>();
                    }
                    previousBookId = currentBookId;
                }

                // if it's a top level topic, add it to book's topics collection
                if (topic.ParentTopicId == null)
                {
                    currentBook.Topics.Add(topic);
                }

                topic.Topics = new List<Topic>();
                dic.Add(topic.TopicId, topic);

            }
            #endregion

            #region build tree, find each topic's parent

            foreach (var topic in topics)
            {
                var parentId = topic.ParentTopicId;
                if (parentId != null)
                {
                    var parent = dic[parentId.Value];
                    parent.Topics.Add(topic);
                }

                // initialize article collection
                if (topic.Articles == null)
                    topic.Articles = new List<Article>();
            }

            #endregion

            #region attach articles
            var articles = _articleRep.GetArticles(portalId).OrderByDescending(a => a.DisplayOrder);
            foreach (var article in articles)
            {
                dic[article.TopicId].Articles.Add(article);
            }
            #endregion

            Domain.Portals.Portal portal = _portalRep.GetById(portalId);
        //    Domain.Portals.Portal portal = new Portals.Portal() { Title = "kkk" };
            portal.Books = books;
            return portal;
        }
    }
}
