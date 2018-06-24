using DAL.App.Interfaces.Interfaces;
using DAL.Repository;
using Domains;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAL.App.EF.Repositories
{
    public class DiseasesRepository : EFRepository<Disease>, IDiseasesRepository
    {
        public DiseasesRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public List<Disease> AllWithSymptoms()
        {
            return repoDbSet.Include(x => x.SymptomsInDiseases)
                .ThenInclude(s => s.Symptom)
                .ToList();
        }

        public bool Exists(Disease disease)
        {
            return repoDbSet.Where(x => x.Name == disease.Name).Any();
        }
    }
}
