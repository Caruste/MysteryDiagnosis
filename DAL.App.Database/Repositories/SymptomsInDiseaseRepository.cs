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

        /// <summary>
        /// This method is used to get all the references to symptoms from this repository
        /// This is done to get the most frequent symptoms. 
        /// </summary>
        /// <returns>List of all the the symptoms that have been inserted into this table</returns>
        public IEnumerable<SymptomsInDiseases> AllReferencesToSymptoms()
        {
            return repoDbSet.Include(x => x.Symptom);
        }
    }
}
