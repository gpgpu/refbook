using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Articles
{
    public interface IArticleRepository : IRepository
    {
        IQueryable<Article> GetArticles(int portalid);
        IQueryable<Article> GetArticlesWithoutContent(int topicId);
        Article GetArticleWithContent(long articleId);

        long Create(Article newArticle);
        void SaveContent(int articleid, string content);
    }
}
