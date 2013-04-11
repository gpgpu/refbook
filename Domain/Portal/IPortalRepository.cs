using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;

namespace Domain.Portals
{
    public interface IPortalRepository : IRepository
    {
        Portal GetById(int portalId);
        IQueryable<Portal> GetAll();

        void Insert(Portal portal);
        void Update(Portal portal);
        void Delete(int portalId);
    }
}
