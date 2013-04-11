using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;
using Domain.Books;


namespace Infrastructure.Data
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public IQueryable<Book> GetBooks(int portalId)
        {
            var oq = base.GetObjectQuery<Book>();
            return oq.Where(b => b.PortalId == portalId).OrderByDescending(b => b.DisplayOrder);
        }
    }
}
