using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> All();

        void Add(TEntity entity);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
