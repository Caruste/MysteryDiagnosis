using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.Interfaces
{
    /// <summary>
    /// This interface is used to tell EFRepisotry which methods should be used.
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> All();
        void Add(TEntity entity);
        void SaveChanges();
        void RemoveDiseasesAndSymptoms();
    }
}
