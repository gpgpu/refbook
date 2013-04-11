using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Domain.Portals;
using Infrastructure.Data;

namespace Application.Services.Controllers
{
    public class PortalController : ApiController
    {
        private IPortalRepository _portalRep;
        public PortalController(IPortalRepository portalRep)
        {
            this._portalRep = portalRep;
        }
        public IEnumerable<Portal> Get()
        {
            return _portalRep.GetAll().ToList();
        }
    }
}
