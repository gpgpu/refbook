using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Topics
{
    public interface ITopicRepository : IRepository
    {
        IQueryable<Topic> GetAllTopics(int portalId);

        int Create(Topic newTopic);
    }
}
