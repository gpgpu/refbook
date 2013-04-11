using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Domain.Topics;
using Infrastructure.Data;

namespace WebUI.Controllers
{
    public class TopicController : Controller
    {
        [HttpPost]
        public int Create(Topic newTopic)
        {
            var rep = new Infrastructure.Data.dapperTopicRepository();

            return rep.Create(newTopic);

        }

    }
}
