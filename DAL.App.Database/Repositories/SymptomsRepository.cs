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
    public class SymptomsRepository : EFRepository<Symptom>, ISymptomsRepository
    {
        public SymptomsRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Symptom FindByName(string name)
        {
            return repoDbSet.Where(x => x.Name == name).FirstOrDefault();
        }

        public int SymptomsCount()
        {
            return repoDbSet.Count();
        }
    }
}
