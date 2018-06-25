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
    [Produces("application/json")]
    [Route("api/Diseases")]
    public class DiseasesController : Controller
    {
        private readonly IDiseasesService _diseasesService;
        private readonly ISymptomService _symptomService;
        public DiseasesController(IDiseasesService diseasesService, ISymptomService symptomService)
        {
            _diseasesService = diseasesService;
            _symptomService = symptomService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(
                _diseasesService.AllWithSymptoms()
                .Select( x => DiseaseDTO.Transform(x))
                .ToList());
        }

        [HttpPost]
        public IActionResult AddToDatabase([FromBody] List<string> input)
        {
            _diseasesService.AddAll(input);
            // Returning an empty string otherwise it wouldn't count as a success.
            return Ok("");
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