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
        public IActionResult Statistics()
        {
            return View();
        }

        public IActionResult Database()
        {
            return View();
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