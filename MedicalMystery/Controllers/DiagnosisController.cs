using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.Database;
using DAL.App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalMystery.Controllers.api
{
    public class DiagnosisController : Controller
    {
        private readonly DbContext _dataContext;
        public DiagnosisController(IDataContext context)
        {
            _dataContext = context as DbContext;
        }
        public IActionResult Index()
        {
            return View(
                _dataContext.All()
                    .OrderByDescending(x => x.symptoms.Count)
                    .ThenBy(x => x.name)
                    .Take(3)
                );
        }

        public IActionResult Database()
        {
            return View(_dataContext.All());
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