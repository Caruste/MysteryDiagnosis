using BL.Interfaces;
using DAL.App.Interfaces.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Services
{
    public class SymptomService : ISymptomService
    {
        private readonly ISymptomsRepository _symptomRepo;
        private readonly ISymptomsInDiseaseRepository _sidRepository;
        public SymptomService(ISymptomsRepository symptomsRepo, ISymptomsInDiseaseRepository symptomsInDiseaseRepository)
        {
            _symptomRepo = symptomsRepo;
            _sidRepository = symptomsInDiseaseRepository;
        }

        public void AddSymptom(Symptom symptom)
        {
            _symptomRepo.Add(symptom);
            _symptomRepo.SaveChanges();
        }

        public IEnumerable<Symptom> AllSymptoms()
        {
            return _symptomRepo.All();
        }

        public Symptom FindByName(Symptom symptom)
        {
            return FindByName(symptom.Name);
        }

        public Symptom FindByName(string name)
        {
            return _symptomRepo.FindByName(name);
        }

        public IEnumerable<string> TopThreeSymptoms()
        {
            IEnumerable<SymptomsInDiseases> symptomsInDiseases = _sidRepository.AllSymptomIds();
            if (symptomsInDiseases == null) return null;

            Dictionary<string, int> symptoms = new Dictionary<string, int>();
            foreach (var item in symptomsInDiseases)
            {
                if (symptoms.ContainsKey(item.Symptom.Name)) symptoms[item.Symptom.Name]++;
                else symptoms.Add(item.Symptom.Name, 0);
            }

            var sortedDictionary = symptoms
                                    .OrderByDescending(x => x.Value)
                                    .ThenBy(x => x.Key)
                                    .Select(x => x.Key.ToString())
                                    .Take(3)
                                    .ToList();

            return sortedDictionary;
        }

        public int UniqueSymptomsCount()
        {
            return _symptomRepo.SymptomsCount();
        }

    }
}
