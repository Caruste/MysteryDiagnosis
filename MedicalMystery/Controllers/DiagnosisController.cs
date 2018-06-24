using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.App.Database;
using DAL.App.Interfaces;
using DAL.App.Interfaces.Interfaces;
using Domains;
using Microsoft.AspNetCore.Mvc;

namespace MedicalMystery.Controllers.api
{
    public class DiagnosisController : Controller
    {
        private readonly IDiseasesService _diseasesService;
        public DiagnosisController(IDiseasesService diseasesService)
        {
            _diseasesService = diseasesService;
        }
        public IActionResult Index()
        {
#warning Make this method return empty View! 

            return View(
                _diseasesService.AllWithSymptoms()
                .OrderByDescending(x => x.Symptoms.Count)
                .ThenBy(x => x.Name)
                .Take(3)
                );
        }

        public IActionResult Database()
        {
#warning Make this method return empty View! 

            return View(_diseasesService.AllWithSymptoms());
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