using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Books;

namespace Domain.Portals
{
    public class Portal
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int? DisplayOrder { get; set; }

        public virtual List<Book> Books { get; set; }

    }
}
