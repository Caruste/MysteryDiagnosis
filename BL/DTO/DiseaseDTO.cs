using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.DTO
{
    /// <summary>
    /// This class is used to transfer Diseases from backend to frontend.
    /// This reduces the amount of information sent and lists symptoms as strings
    /// </summary>
    public class DiseaseDTO
    {
        public string name;
        public List<string> symptoms;


        /// <summary>
        /// This method transforms a Disease object to DiseaseDTO object
        /// </summary>
        /// <param name="disease">Disease which we wish to transform</param>
        /// <returns>DiseaseDTO which was transformed from Disease object</returns>
        public static DiseaseDTO Transform(Disease disease)
        {
            return new DiseaseDTO()
            {
                name = disease.Name,
                symptoms = disease.Symptoms.Select(x => x.Name).ToList()
            };
        }
    }
}
