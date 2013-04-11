using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Articles;
using Domain.Books;


namespace Domain.Topics
{
    public class Topic 
    {
        public int TopicId { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; } 

        public string Title { get; set; }
   
        public int? ParentTopicId { get; set; }
        public virtual Topic ParentTopic { get; set; }

        public virtual List<Topic> Topics { get; set; }
        public virtual List<Article> Articles { get; set; }

        public int? DisplayOrder { get; set; }

           
    }
}
