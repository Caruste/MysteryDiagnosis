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
        public TestController(IDiseasesRepository diseasesRepository, IDiseasesService diseasesService)
        {
            diseasesRepository = diseasesRepository;
            _diseasesService = diseasesService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_diseasesRepository.All());
        }

        [HttpGet("Amount")]
        public IActionResult symptomCount()
        {
            IEnumerable<Disease> diseases = _diseasesRepository.All();
            List<string> symptoms = new List<string>();

            foreach (var disease in diseases)
            {
                foreach (var symptom in disease.SymptomString)
                {
                    if (!symptoms.Contains(symptom)) symptoms.Add(symptom);
                }
            }

            return Ok(symptoms.Count);
        }

        [HttpGet("Symptoms")]
        public List<string> topSymptoms()
        {
            IEnumerable<Disease> diseases = _diseasesRepository.All();
            Dictionary<string, int> symptomCount = new Dictionary<string, int>();

            foreach (var disease in diseases)
            {
                foreach (var symptom in disease.SymptomString)
                {
                    if (symptomCount.ContainsKey(symptom)) symptomCount[symptom]++;
                    else symptomCount.Add(symptom, 0);
                }
            }

            var sortedDictionary = symptomCount
                                    .OrderByDescending(x => x.Value)
                                    .ThenBy(x => x.Key)
                                    .Select(x => x.Key.ToString())
                                    .Take(3)
                                    .ToList();
            return sortedDictionary;
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
            if (!System.IO.File.Exists("database/database.csv")) return NotFound();
            System.IO.File.WriteAllLines("database/database.csv", input);
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