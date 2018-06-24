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
    [Route("api/Symptoms")]
    public class SymptomsController : Controller
    {
        private readonly IDiseasesService _diseasesService;
        private readonly ISymptomService _symptomService;
        public SymptomsController(IDiseasesService diseasesService, ISymptomService symptomService)
        {
            _diseasesService = diseasesService;
            _symptomService = symptomService;
        }

        [HttpGet("Top")]
        public IActionResult topSymptoms()
        {
            var response = _symptomService.TopThreeSymptoms();
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpGet("Amount")]
        public IActionResult symptomCount()
        {
            return Ok(_symptomService.UniqueSymptomsCount());
        }

        [HttpPost("Question")]
        public IActionResult GetNextQuestion([FromBody] AnswersDTO input)
        {
            var alpha = _symptomService.GetNextQuestion(input);
            if (alpha.GetType() == typeof(string)) return Ok(alpha);
            else if (alpha == null) return NotFound();
            return Ok(alpha);
        }

        [HttpPost]
        public IActionResult CheckSymptoms([FromBody] List<string> list)
        {
            return Ok(_symptomService.CheckSymptoms(list));
        }
    }
}