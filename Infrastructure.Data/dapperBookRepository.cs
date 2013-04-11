using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using Dapper;
using Domain;
using Domain.Books;

using ViewModels;


namespace Infrastructure.Data
{
    public class dapperBookRepository : dapperBaseRepository,  IBookRepository
    {
        public IQueryable<Book> GetBooks(int portalId)
        {
            using (SqlConnection conn = base.GetConnection())
            {
                return conn.Query<Book>("select * from Books WHERE portalId=@id ORDER BY DisplayOrder", new { id=portalId}).AsQueryable();
            }
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void SaveItemsOrder(List<ContentItem> items)
        {
            if (items == null) return;

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < items.Count; index++)
            {
                var item = items[index];
                string displayOrder = (index + 1).ToString();

                if (item.isTopic)
                {
                    sb.Append("UPDATE Topics SET DisplayOrder=");
                    sb.Append(displayOrder);
                    sb.Append(" WHERE TopicId=");
                    sb.Append(item.id.ToString());
                    sb.Append("; ");
                }
                else
                {
                    sb.Append("UPDATE Articles SET DisplayOrder=");
                    sb.Append(displayOrder);
                    sb.Append(" WHERE ArticleId=");
                    sb.Append(item.id.ToString());
                    sb.Append("; ");
                }
            }

            using (var conn = base.GetConnection(true))
            {
                conn.Execute(sb.ToString());
            }
        }
    }
}
