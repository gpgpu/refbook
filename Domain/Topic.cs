using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Name { get; set; }
        public List<Topic> SubTopics { get; set; }
        public int? ParentId { get; set; }
        public int? DisplayOrder { get; set; }

        public int BookID_FK { get; set; }

        //public IEnumerable<IOrganizable> GetContentItems(ITopicRepository topicRep, IArticleRepository arRep)
        //{
        //    List<IOrganizable> result = new List<IOrganizable>();
        //    result.AddRange(topicRep.GetSubTopics(this.TopicId));
        //    result.AddRange(arRep.GetArticleList(this.TopicId));

        //    return result.OrderBy(x => x.DisplayOrder);
        //}

        public static void GenereateTree(List<Topic> topics)
        {
            Dictionary<int, Topic> dic = new Dictionary<int, Topic>();
            foreach (var topic in topics)
            {
                dic.Add(topic.TopicId, topic);
            }

            foreach (var topic in topics)
            {
                if (topic.ParentId != null)
                {
                    var parentTopic = dic[topic.ParentId.Value];
                    if (parentTopic.SubTopics == null)
                        parentTopic.SubTopics = new List<Topic>();
                    parentTopic.SubTopics.Add(topic);

                }
            }
        }
    }
}
