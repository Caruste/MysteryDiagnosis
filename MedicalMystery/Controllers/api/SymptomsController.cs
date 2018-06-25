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
        private readonly ISymptomService _symptomService;
        public SymptomsController(ISymptomService symptomService)
        {
            _symptomService = symptomService;
        }

        [HttpGet("Top")]
        public IActionResult topSymptoms()
        {
            return Ok(_symptomService.TopThreeSymptoms());
        }

        [HttpGet("Amount")]
        public IActionResult symptomCount()
        {
            return Ok(_symptomService.UniqueSymptomsCount());
        }

        [HttpPost("Question")]
        public IActionResult GetNextQuestion([FromBody] AnswersDTO input)
        {
            return Ok(_symptomService.GetNextQuestion(input));
        }

        [HttpPost]
        public IActionResult CheckSymptoms([FromBody] List<string> list)
        {
            return Ok(_symptomService.CheckSymptoms(list));
        }
    }
}