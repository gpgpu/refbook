using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Models;

namespace Domain.Articles
{
    public class Article 
    {
        public long ArticleId { get; set; }

        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }

        public string Title { get; set; }
        public string Summary { get; set; }
        public string Keywords { get; set; }

        public string ArticleContent { get; set; }
        
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int? DisplayOrder { get; set; }

        

    }
}
