using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.Database;
using DAL.App.Interfaces;
using DAL.App.Interfaces.Interfaces;
using Domains;
using Microsoft.AspNetCore.Mvc;

namespace MedicalMystery.Controllers.api
{
    public class DiagnosisController : Controller
    {
        private readonly IDiseasesRepository _diseasesRepository;
        public DiagnosisController(IDiseasesRepository diseasesRepository)
        {
            _diseasesRepository = diseasesRepository;
        }
        public IActionResult Index()
        {
#warning Make this method return empty View! 

            return View(_diseasesRepository.All());

            //return View(
            //    _dataContext.All()
            //        .OrderByDescending(x => x.SymptomString.Count)
            //        .ThenBy(x => x.Name)
            //        .Take(3)
            //    );
        }

        public IActionResult Database()
        {
#warning Make this method return empty View! 
            var database = _diseasesRepository.AllWithSymptoms();

            List<Disease> diseases = new List<Disease>();
            foreach (var item in database)
            {
                item.Symptoms = item.SymptomsInDiseases.Select(x => x.Symptom).ToList();
                diseases.Add(item);
            }
            //database.ForEach(x => x.Symptoms.AddRange(x.SymptomsInDiseases.Select(s => s.Symptom))); 

            return View(diseases);
        }

        public IActionResult CheckSymptoms()
        {
            return View();
        }
        public IActionResult SymptomQuestion()
        {
            return View();
        }
    }
}