using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return Ok(_diseasesService.All());
        }

        [HttpPost]
        public IActionResult AddToDatabase([FromBody] List<string> input)
        {
            _diseasesService.AddAll(input);
            return Ok();
        }
    }
}