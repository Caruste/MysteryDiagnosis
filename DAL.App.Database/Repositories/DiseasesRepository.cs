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

        /// <summary>
        /// Gets all Diseases from the database and includes the SymptomsInDiseases to it. 
        /// After including the SymptomsInDiseases it also includes the symptoms in it. 
        /// </summary>
        /// <returns>List of all the Diseases with Symptoms in SymptomsInDiseases property.</returns>
        public IEnumerable<Disease> AllWithSymptoms()
        {
            return repoDbSet
                .Include(x => x.SymptomsInDiseases)
                .ThenInclude(s => s.Symptom);
        }


        /// <summary>
        /// Checks if e Disease already exists in the database by its name
        /// </summary>
        /// <param name="disease">Disease which we are looking for</param>
        /// <returns>Boolean value of whether or not the disease already exists.</returns>
        public bool Exists(Disease disease)
        {
            return repoDbSet.Where(x => x.Name == disease.Name).Any();
        }
    }
}
