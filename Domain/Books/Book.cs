using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Domain.Portals;
using Domain.Topics;

namespace Domain.Books
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int? DisplayOrder { get; set; }

        public int PortalId { get; set; }
   //     public virtual Portal Portal { get; set; }

        public virtual List<Topic> Topics { get; set; }
    }

    
}
