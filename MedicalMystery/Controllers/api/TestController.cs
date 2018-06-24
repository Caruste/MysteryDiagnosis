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
        private readonly IDiseasesRepository _diseasesRepository;
        private readonly IDiseasesService _diseasesService;
        private readonly ISymptomService _symptomService;
        public TestController(IDiseasesRepository diseasesRepository, IDiseasesService diseasesService, ISymptomService symptomService)
        {
            _diseasesRepository = diseasesRepository;
            _diseasesService = diseasesService;
            _symptomService = symptomService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_diseasesRepository.All());
        }

        [HttpGet("Amount")]
        public IActionResult symptomCount()
        {
            return Ok(_symptomService.UniqueSymptomsCount());
        }

        [HttpGet("Symptoms")]
        public List<string> topSymptoms()
        {
#warning This is not finished yet! Must add a way to get frequency of symptoms from ManyToMany table

            //var sortedDictionary = symptomCount
            //                        .OrderByDescending(x => x.Value)
            //                        .ThenBy(x => x.Key)
            //                        .Select(x => x.Key.ToString())
            //                        .Take(3)
            //                        .ToList();
            return _symptomService.TopThreeSymptoms().ToList();
        }

        [HttpPost]
        public IActionResult CheckSymptoms([FromBody] List<string> list)
        {
            if (list.Count == 0) return NotFound();
            IEnumerable<Disease> diseases = _diseasesRepository.All();
            List<Disease> final = new List<Disease>();

            foreach (Disease item in diseases)
            {
                if (ContainsAllItems(item.SymptomString, list)) final.Add(item);
            }
            return Ok(final);
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
            IEnumerable<Disease> diseases = _diseasesRepository.All();
            List<Disease> hasAll = new List<Disease>();

            if (input.positive != null)
            {
                if (input.positive.Count == 0) hasAll = diseases.ToList();
                else hasAll = diseases.Where(x => ContainsAllItems(x.SymptomString, input.positive)).ToList();
            }

            List<Disease> final = new List<Disease>();
            
            if (input.negative != null)
            {
                if (input.negative.Count == 0) final = hasAll;
                else
                    // This checks if there are any same symptoms in the disease and input negative. 
                    // Disease can't contain anything form the input.negative as the user has answered no to it!
                    final = hasAll.Where(x => !x.SymptomString.Intersect(input.negative).Any()).ToList();
            }

            var list = final.OrderBy(x => x.SymptomString.Count).ToList();
            if (list.Count() <= 1) return Ok(list.FirstOrDefault());

            List<string> symptomList = list.ElementAt(1).SymptomString.Except(list.FirstOrDefault().SymptomString).ToList();

            foreach (var item in symptomList)
            {
                if (!input.positive.Contains(item) && !input.negative.Contains(item)) return Ok(item);
            }

            return NotFound();
        }

        public static bool ContainsAllItems(List<string> a, List<string> b)
        {
            return !b.ConvertAll(x => x.ToLower()).Except(a.ConvertAll(x => x.ToLower())).Any();
        }
    }
}