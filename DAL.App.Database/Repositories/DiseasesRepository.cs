using DAL.App.Interfaces.Interfaces;
using DAL.Repository;
using Domains;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.App.Database.Repositories
{
    public class DiseasesRepository : EFRepository<Disease>, IDiseasesRepository
    {
        public DiseasesRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public bool Exists(Disease disease)
        {
            return repoDbSet.Where(x => x.Name == disease.Name).Any();
        }
    }
}
