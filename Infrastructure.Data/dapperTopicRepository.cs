using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Topics;

namespace Infrastructure.Data
{
    public class dapperTopicRepository : dapperBaseRepository, ITopicRepository
    {

        public IQueryable<Topic> GetAllTopics(int portalId)
        {
            using (var conn = base.GetConnection())
            {
                var topics = conn.Query<Topic>("SELECT * FROM  Topics WHERE bookid in (SELECT Id FROM books WHERE portalId=@portalid) ORDER BY bookId, displayOrder", new { portalId = portalId });

                return topics.AsQueryable();
            }
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }


        public int Create(Topic newTopic)
        {
            string sql = @"INSERT INTO Topics(Title, BookId, ParentTopicId) VALUES(@title, @bookId, @parentId); SELECT CAST(scope_identity() as int);";

            using (var conn = base.GetConnection(true))
            {
                return conn.Query<int>(sql, new {title=newTopic.Title, bookId=newTopic.BookId, parentId=newTopic.ParentTopicId }).First();
            }
        }
    }
}
