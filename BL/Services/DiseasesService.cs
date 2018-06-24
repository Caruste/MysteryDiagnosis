using BL.Interfaces;
using DAL.App.Interfaces.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BL.Services
{
    public class DiseasesService : IDiseasesService
    {

        private readonly IDiseasesRepository _diseasesRepo;
        private readonly ISymptomsRepository _symptomsRepo;

        public DiseasesService(IDiseasesRepository diseasesRepository, ISymptomsRepository symptomsRepository)
        {
            _diseasesRepo = diseasesRepository;
            _symptomsRepo = symptomsRepository;
        }
        public void AddAll(List<string> list)
        {
            List<Disease> diseases = new List<Disease>();
            foreach (var item in list)
            {
                diseases.Add(StringMutation.DiseaseFromString(item));
            }

            int i = 0; 
            foreach (Disease disease in diseases)
            {
                foreach (Symptom symptom in disease.Symptoms)
                {
                    Symptom temp = symptom;
                    if (_symptomsRepo.Exists(symptom)) temp = _symptomsRepo.FindByName(symptom);
                    else
                    {
                        _symptomsRepo.Add(temp);
                        _symptomsRepo.SaveChanges();
                    }
                    disease.SymptomsInDiseases.Add(new SymptomsInDiseases()
                    {
                        Disease = disease,
                        Symptom = temp,
                        SymptomsInDiseasesId = i++
                    });
                }
                if (!_diseasesRepo.Exists(disease)) _diseasesRepo.Add(disease);
                _diseasesRepo.SaveChanges();
            }
        }
    }
}
