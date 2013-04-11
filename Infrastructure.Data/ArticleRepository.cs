using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using Domain.Articles;

namespace Infrastructure.Data
{
    public class ArticleRepository : dapperBaseRepository, IArticleRepository
    {
        public IQueryable<Article> GetArticles(int portalId)
        {
            string sql = "SELECT articleId, TopicId, Title, DisplayOrder FROM Articles WHERE TopicId in (select topicid from Topics where BookId in (select Id FROM Books WHERE portalId=@portalId)) ORDER BY DisplayOrder; ";
            using (var conn = base.GetConnection(true))
            {
                var articles = conn.Query<Article>(sql, new {portalId=portalId });
                return articles.AsQueryable();
            }
        }

        public IQueryable<Article> GetArticlesWithoutContent(int topicId)
        {
            string sql = "SELECT ArticleId, TopicId, Title, DisplayOrder FROM Articles WHERE TopicId=@topicid;";
            using (var conn = base.GetConnection(true))
            {
                var articles = conn.Query<Article>(sql, new {topicId=topicId });
                return articles.AsQueryable();
            }
        }

        public Article GetArticleWithContent(long articleId)
        {
            string sql = "SELECT * FROM Articles WHERE articleId=@articleId;";
            using (var conn = base.GetConnection(true))
            {
                return conn.Query<Article>(sql, new {articleid=articleId }).FirstOrDefault();
                
            }
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }


        public void SaveContent(int articleid, string content)
        {
            string sql = @"UPDATE Articles SET ArticleContent=@content WHERE ArticleId=@articleId;";
            using (var conn = base.GetConnection(true))
            {
                conn.Execute(sql, new {content=content, articleid=articleid });
            }
        }


        public long Create(Article newArticle)
        {
            string sql = @"INSERT INTO Articles(Topicid, Title, DisplayOrder) VALUES(@topicId, @articleTitle, @displayOrder) select cast(scope_identity() as bigint)";
            using (var conn = base.GetConnection(true))
            {
                 return conn.Query<long>(sql, new { topicId = newArticle.TopicId, articleTitle = newArticle.Title, displayOrder=newArticle.DisplayOrder }).First();
            }
        }
    }
}
