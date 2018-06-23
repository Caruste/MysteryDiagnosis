using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> All();
    }
}
