using BL.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class SymptomsInDiseaseService : ISymptomsInDiseaseService
    {
        public SymptomsInDiseases createNew(Symptom symptom, Disease disease, int i)
        {
            return new SymptomsInDiseases()
            {
                Symptom = symptom,
                Disease = disease,
                SymptomsInDiseasesId = i
            };
        }
    }
}
