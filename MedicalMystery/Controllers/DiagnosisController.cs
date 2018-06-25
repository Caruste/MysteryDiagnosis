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
    /// <summary>
    /// This class is used to return views. 
    /// No information goes through here
    /// This is done so that frontend could be changed 
    ///     out easily to something like React or Angular webpage.
    /// </summary>
    public class DiagnosisController : Controller
    {
        /// <summary>
        /// This is the main page of the frontend. 
        /// All the statistics will be stored there.
        /// </summary>
        /// <returns>View of the statistics (Statistics.cshtml)</returns>
        public IActionResult Statistics()
        {
            return View();
        }

        /// <summary>
        /// This is used to return the database page, where you can 
        ///     add or remove information from the database.
        /// </summary>
        /// <returns>View of the database (Database.cshtml)</returns>
        public IActionResult Database()
        {
            return View();
        }

        /// <summary>
        /// This is used to return the symptons page, where you can 
        ///     check for diseases by typing the symptoms.
        /// </summary>
        /// <returns>View of the symptoms checking page (CheckSymptoms.cshtml)</returns>
        public IActionResult CheckSymptoms()
        {
            return View();
        }


        /// <summary>
        /// This is used to return the symptons questioning page, where you can 
        ///     check for diseases by pressing yes or no to symptoms.
        /// </summary>
        /// <returns>View of the symptoms questioning page (SymptomQuestion.cshtml)</returns>
        public IActionResult SymptomQuestion()
        {
            return View();
        }
    }
}