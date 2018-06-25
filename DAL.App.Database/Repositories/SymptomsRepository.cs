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
        /// <summary>
        /// This method finds a symptom from the database by its name
        /// </summary>
        /// <param name="name">Name of the symptom which we wish to find.</param>
        /// <returns>Symptom, which was found in the database or null if isn't found.</returns>
        public Symptom FindByName(string name)
        {
            return repoDbSet.Where(x => x.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// This method gets the count of symptoms in the symptoms table
        /// </summary>
        /// <returns>Number of symptoms that are in the table.</returns>
        public int SymptomsCount()
        {
            return repoDbSet.Count();
        }
    }
}
