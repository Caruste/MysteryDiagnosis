using DAL.App.Interfaces.Interfaces;
using DAL.Repository;
using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.App.EF.Repositories
{
    public class SymptomsInDiseaseRepository : EFRepository<SymptomsInDiseases> , ISymptomsInDiseaseRepository
    {
        public SymptomsInDiseaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<SymptomsInDiseases> AllSymptomIds()
        {
            return repoDbSet.Include(x => x.Symptom);
        }
    }
}
