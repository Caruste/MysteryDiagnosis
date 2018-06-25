using DAL.Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext repoDbContext;
        protected DbSet<TEntity> repoDbSet;
        public EFRepository(DbContext dbContext)
        {
            repoDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            repoDbSet = dbContext.Set<TEntity>();

            if (repoDbSet == null)
            {
                throw new ArgumentException("Couldn't find DbSet in datacontext");
            }
        }

        public void Add(TEntity entity)
        {
            repoDbSet.Add(entity);
        }

        public IEnumerable<TEntity> All()
        {
            return repoDbSet.ToList();
        }

        public void RemoveDiseasesAndSymptoms()
        {
            repoDbContext.Database.ExecuteSqlCommand("DELETE FROM Symptoms");
            repoDbContext.Database.ExecuteSqlCommand("DELETE FROM Diseases");
        }

        public void SaveChanges()
        {
            repoDbContext.SaveChanges();
        }
    }
}
