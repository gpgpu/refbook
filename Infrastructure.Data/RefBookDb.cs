using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Books;
using Domain.Portals;
using Domain.Topics;

namespace Infrastructure.Data
{
    public class RefBookDb : DbContext, IUnitOfWork
    {
        public DbSet<Portal> Portals { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Topic> Topics { get; set; }

      

        
        public void Commit()
        {
            this.SaveChanges();
        }
    }
}
