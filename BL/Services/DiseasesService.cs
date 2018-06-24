using BL.Interfaces;
using DAL.App.Interfaces.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BL.Services
{
    public class DiseasesService : IDiseasesService
    {

        private readonly IDiseasesRepository _diseasesRepo;
        public DiseasesService(IDiseasesRepository _diseasesRepository)
        {
            _diseasesRepo = _diseasesRepository;
        }
        public void AddAll(List<string> list)
        {
            List<Disease> diseases = new List<Disease>();
            foreach (var item in list)
            {
                Disease disease = StringMutation.DiseaseFromString(item);
                diseases.Add(disease);
                //if (!_diseasesRepo.Exists(disease)) _diseasesRepo.Add(disease);
            }

            foreach (Disease disease in diseases)
            {
                foreach (Symptom symptom in disease.Symptoms)
                {

                }
            }
        }
    }
}
