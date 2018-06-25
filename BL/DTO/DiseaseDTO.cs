using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.DTO
{
    public class DiseaseDTO
    {
        public string name;
        public List<string> symptoms;

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
