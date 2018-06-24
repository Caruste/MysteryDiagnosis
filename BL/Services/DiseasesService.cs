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
        private readonly ISymptomService _symptomsService;
        private readonly ISymptomsInDiseaseService _sidService;

        public DiseasesService(IDiseasesRepository diseasesRepository, ISymptomService symptomsService, ISymptomsInDiseaseService symptomsInDiseaseService)
        {
            _diseasesRepo = diseasesRepository;
            _symptomsService = symptomsService;
            _sidService = symptomsInDiseaseService;
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
                if (_diseasesRepo.Exists(disease)) continue;
                foreach (Symptom symptom in disease.Symptoms)
                {
                    Symptom temp = _symptomsService.FindByName(symptom);
                    if (temp == null) temp = symptom;
                    else _symptomsService.AddSymptom(temp);

                    disease.SymptomsInDiseases.Add(_sidService.createNew(temp, disease, i++));
                }
                _diseasesRepo.Add(disease);
                _diseasesRepo.SaveChanges();

            }
        }

        public List<Disease> AllWithSymptoms()
        {
            List<Disease> diseases = new List<Disease>();
            foreach (var item in _diseasesRepo.AllWithSymptoms())
            {
                item.Symptoms = item.SymptomsInDiseases.Select(x => x.Symptom).ToList();
                diseases.Add(item);
            }

            return diseases;
        }
    }
}
