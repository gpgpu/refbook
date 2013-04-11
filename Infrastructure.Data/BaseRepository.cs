using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Infrastructure.Data
{
    public class BaseRepository : IRepository, IDisposable
    {
        private IUnitOfWork _unitOfWork;
        protected RefBookDb _db;
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _db = (RefBookDb)_unitOfWork;
        }

      
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        protected virtual DbSet<T> GetObjectQuery<T>() where T : class
        {
            return this._db.Set<T>();
        }
        protected virtual void SetEntityState(object entity, EntityState state)
        {
            this._db.Entry(entity).State = state;
        }
    }
}
