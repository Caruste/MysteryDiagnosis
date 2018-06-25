using BL.DTO;
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
        private readonly ISymptomsRepository _symptomsRepository;
        private readonly ISymptomsInDiseaseService _sidService;

        public DiseasesService(IDiseasesRepository diseasesRepository, ISymptomsRepository symptomsRepository, ISymptomsInDiseaseService symptomsInDiseaseService)
        {
            _diseasesRepo = diseasesRepository;
            _symptomsRepository = symptomsRepository;
            _sidService = symptomsInDiseaseService;
        }

        public void AddAll(List<string> list)
        {
            List<Disease> diseases = list.Select(x => StringMutation.DiseaseFromString(x)).ToList();

            /*  Looping through the diseases to add them all into the database.
             * 
             *  First we check if the Disease already exists in the database, if it does
             *  then we move straight to another disease.
             *  
             *  If The disease doesnt exist then we loop through the symptoms.
             *  First we check if the symptom exists, if not then we add it to the database.
             *  Then we add SymptomsInDiseases to the Disease using the disease and symptom. 
             *  
             *  Finally we add the disease to the database and savechanges.
             */ 
            foreach (Disease disease in diseases)
            {
                if (_diseasesRepo.Exists(disease)) continue;
                foreach (Symptom symptom in disease.Symptoms)
                {
                    Symptom temp = _symptomsRepository.FindByName(symptom.Name);
                    if (temp == null)
                    {
                        temp = symptom;
                        _symptomsRepository.Add(temp);
                    }

                    disease.SymptomsInDiseases.Add(_sidService.createNew(temp, disease));
                }
                _diseasesRepo.Add(disease);
                _diseasesRepo.SaveChanges();
            }
        }

        public IEnumerable<DiseaseDTO> AllWithSymptoms()
        {

            /*  First we everything from diseasesRepo which includes SymptomsInDiseases
            *   Then we Select the SymptomsInDiseases, take out the symptom object and add
            *   it to the original object symptoms List and we do it with everything in 
            *   SymptomsInDiseases. 
            *   Then we make it into a list and return it.
            */
            return _diseasesRepo
                        .AllWithSymptoms()
                        .Select(x => { x.Symptoms = x.SymptomsInDiseases.Select(s => s.Symptom).ToList(); return x; })
                        .Select(s => DiseaseDTO.Transform(s));
        }

        public void ClearDiseasesAndSymptomsDBFORTESTING()
        {
            _diseasesRepo.RemoveDiseasesAndSymptoms();
        }

        public IEnumerable<DiseaseDTO> TopThreeBySymptomsCount()
        {
            return AllWithSymptoms()
                .OrderByDescending(x => x.symptoms.Count)
                .ThenBy(x => x.name)
                .Take(3);
        }
    }
}
