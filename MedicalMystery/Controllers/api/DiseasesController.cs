using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalMystery.Controllers.api
{

    /// <summary>
    /// This controller is used to return anything diseases related
    /// To access is you must use .../api/diseases/{method}
    /// </summary>
    [Produces("application/json")]
    [Route("api/Diseases")]
    public class DiseasesController : Controller
    {
        private readonly IDiseasesService _diseasesService;
        public DiseasesController(IDiseasesService diseasesService)
        {
            _diseasesService = diseasesService;
        }

        /// <summary>
        /// This is used to get all of the Diseases with symptoms
        /// </summary>
        /// <returns>IEnumerable of DiseaseDTO's which have symptoms included</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_diseasesService.AllWithSymptoms());
        }

        /// <summary>
        /// This method is used to add several Diseases and symptoms to the database
        /// Input form must be following: DiseaseName, Symptom1, Symptom2, ...
        /// </summary>
        /// <param name="input">List of strings which we wish to add to the database. </param>
        /// <returns>StatusCode with an empty string. 
        ///         This is so Ajax would recognize that it should continue with success</returns>
        [HttpPost]
        public IActionResult AddToDatabase([FromBody] List<string> input)
        {
            _diseasesService.AddAll(input);
            // Returning an empty string otherwise it wouldn't count as a success.
            return Ok("");
        }

        /// <summary>
        /// This method is used to get top three Diseases by their symptom count. 
        /// They are ordered by symptom count and then by their name alphabetically.
        /// </summary>
        /// <returns>IEnumerable of top three diseases bysymptoms count </returns>
        [HttpGet("TopThree")]
        public IActionResult Index()
        {
            return Ok(_diseasesService.TopThreeBySymptomsCount());
        }

#warning this method must be deleted if it ever goes live!
        /// <summary>
        /// This method is only created for testing purposes!
        /// If the system is ever to go live this must be removed!
        /// </summary>
        /// <returns>Response statuscode</returns>
        [HttpDelete]
        public IActionResult clearTables()
        {
            _diseasesService.ClearDiseasesAndSymptomsDBFORTESTING();
            return Ok("");
        }
    }
}