using BL.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class SymptomsInDiseaseService : ISymptomsInDiseaseService
    {
        /// <summary>
        /// This method is used to create a new SymptomsInDiseases from symptom and a disease
        /// </summary>
        /// <param name="symptom">Symptom which we will use to create a response</param>
        /// <param name="disease">Disease which we will use to create a response</param>
        /// <returns>SymptomsInDiseases which is made by two given inputs</returns>
        public SymptomsInDiseases createNew(Symptom symptom, Disease disease)
        {
            return new SymptomsInDiseases()
            {
                Symptom = symptom,
                Disease = disease
            };
        }
    }
}
