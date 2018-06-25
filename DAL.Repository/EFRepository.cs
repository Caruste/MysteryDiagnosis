using DAL.Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    /// <summary>
    /// This class is used by other repositories to get information from the database.
    /// This is also used to get RepositoryDbSets from the database. 
    /// </summary>
    /// <typeparam name="TEntity">Class which table we are going to work with from the database</typeparam>
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

        /// <summary>
        /// This method is used to add an entity to the database
        /// </summary>
        /// <param name="entity">Entity which was added to the database</param>
        public void Add(TEntity entity)
        {
            repoDbSet.Add(entity);
        }

        /// <summary>
        /// This method is used to get all of the entities from the d atabase
        /// </summary>
        /// <returns>Returns the list of everything from the database.</returns>
        public IEnumerable<TEntity> All()
        {
            return repoDbSet;
        }

        /// <summary>
        /// This method is used to delete everything from Symptoms and Diseases. 
        /// This is only used for fast testing! Once those two tables are deleted
        /// the many to many table will also be deleted.
        /// </summary>
        public void RemoveDiseasesAndSymptoms()
        {
            repoDbContext.Database.ExecuteSqlCommand("DELETE FROM Symptoms");
            repoDbContext.Database.ExecuteSqlCommand("DELETE FROM Diseases");
        }

        /// <summary>
        /// This method is used to save all of the changes to the database. 
        /// </summary>
        public void SaveChanges()
        {
            repoDbContext.SaveChanges();
        }
    }
}
