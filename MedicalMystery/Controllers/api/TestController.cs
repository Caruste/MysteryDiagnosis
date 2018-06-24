using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Interfaces;
using DAL.App.Database;
using DAL.App.Interfaces;
using DAL.App.Interfaces.Interfaces;
using Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MedicalMystery.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        private readonly IDiseasesService _diseasesService;
        private readonly ISymptomService _symptomService;
        public TestController(IDiseasesService diseasesService, ISymptomService symptomService)
        {
            _diseasesService = diseasesService;
            _symptomService = symptomService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_diseasesService.All());
        }

        [HttpGet("Amount")]
        public IActionResult symptomCount()
        {
            return Ok(_symptomService.UniqueSymptomsCount());
        }

        [HttpGet("Symptoms")]
        public IActionResult topSymptoms()
        {
            var response = _symptomService.TopThreeSymptoms();
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public IActionResult CheckSymptoms([FromBody] List<string> list)
        {
            return Ok(_symptomService.CheckSymptoms(list));
        }



        [HttpPost("Database")]
        public IActionResult CreateDatabase([FromBody] List<string> input)
        {
            _diseasesService.AddAll(input);
            return Ok();
        }

        [HttpPost("ForQuestions")]
        public IActionResult GetNextQuestion([FromBody] AnswersDTO input)
        {
            var alpha = _symptomService.GetNextQuestion(input);
            if (alpha.GetType() == typeof(string)) return Ok(alpha);
            else if (alpha == null) return NotFound();
            return Ok(alpha);
        }
    }
}