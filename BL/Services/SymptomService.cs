using BL.Interfaces;
using DAL.App.Interfaces.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public class SymptomService : ISymptomService
    {
        private readonly ISymptomsRepository _symptomRepo;
        public SymptomService(ISymptomsRepository symptomsRepo)
        {
            _symptomRepo = symptomsRepo;
        }

        public void AddSymptom(Symptom symptom)
        {
            _symptomRepo.Add(symptom);
            _symptomRepo.SaveChanges();
        }

        public Symptom FindByName(Symptom symptom)
        {
            return FindByName(symptom.Name);
        }

        public Symptom FindByName(string name)
        {
            return _symptomRepo.FindByName(name);
        }

        public int UniqueSymptomsCount()
        {
            return _symptomRepo.SymptomsCount();
        }
    }
}
