using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

using Domain;
using Domain.Portals;

namespace Infrastructure.Data
{
    public class PortalRepository : dapperBaseRepository, IPortalRepository
    {
  //      public PortalRepository(IUnitOfWork unitOfwork) : base(unitOfwork) { }

        public Portal GetById(int portalId)
        {
            using (var conn = base.GetConnection(true))
            {
                return conn.Query<Portal>("SELECT * FROM Portals WHERE Id=@portalId", new {@portalId=portalId }).FirstOrDefault();
            }
        }

        public IQueryable<Portal> GetAll()
        {
            using (var conn = base.GetConnection(true))
            {
                var portals = conn.Query<Portal>("SELECT * FROM Portals");
                return portals.AsQueryable();
            }
            //var oq = base.GetObjectQuery<Portal>();
            //return oq.OrderBy(p => p.DisplayOrder);
        }

        public void Insert(Portal portal)
        {
            throw new NotImplementedException();
        }

        public void Update(Portal portal)
        {
            throw new NotImplementedException();
        }

        public void Delete(int portalId)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}
