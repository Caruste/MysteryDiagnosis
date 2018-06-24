using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.Database;
using DAL.App.Interfaces;
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
        private readonly DbContext _dataContext;
        public TestController(IDataContext context)
        {
            _dataContext = context as DbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_dataContext.All());
        }

        [HttpGet("Amount")]
        public IActionResult symptomCount()
        {
            List<Disease> diseases = _dataContext.All();
            List<string> symptoms = new List<string>();

            foreach (var disease in diseases)
            {
                foreach (var symptom in disease.symptoms)
                {
                    if (!symptoms.Contains(symptom)) symptoms.Add(symptom);
                }
            }

            return Ok(symptoms.Count);
        }

        [HttpGet("Symptoms")]
        public List<string> topSymptoms()
        {
            List<Disease> diseases = _dataContext.All();
            Dictionary<string, int> symptomCount = new Dictionary<string, int>();

            foreach (var disease in diseases)
            {
                foreach (var symptom in disease.symptoms)
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
            List<Disease> diseases = _dataContext.All();
            List<Disease> final = new List<Disease>();

            foreach (Disease item in diseases)
            {
                if (ContainsAllItems(item.symptoms, list)) final.Add(item);
            }
            return Ok(final);
        }

        [HttpPost("Database")]
        public IActionResult CreateDatabase([FromBody] List<string> input)
        {
            if (!System.IO.File.Exists("database/database.csv")) return NotFound();
            System.IO.File.WriteAllLines("database/database.csv", input);
            return Ok();
        }

        [HttpPost("ForQuestions")]
        public IActionResult GetNextQuestion([FromBody] Input input)
        {
            List<Disease> diseases = _dataContext.All();
            List<Disease> hasAll = new List<Disease>();

            if (input.positive != null)
            {
                if (input.positive.Count == 0) hasAll = diseases;
                else hasAll = diseases.Where(x => ContainsAllItems(x.symptoms, input.positive)).ToList();
            }

            List<Disease> final = new List<Disease>();
            
            if (input.negative != null)
            {
                if (input.negative.Count == 0) final = hasAll;
                else
                    // This checks if there are any same symptoms in the disease and input negative. 
                    // Disease can't contain anything form the input.negative as the user has answered no to it!
                    final = hasAll.Where(x => !x.symptoms.Intersect(input.negative).Any()).ToList();
            }

            var list = final.OrderBy(x => x.symptoms.Count).ToList();
            if (list.Count() <= 1) return Ok(list.FirstOrDefault());

            List<string> symptomList = list.ElementAt(1).symptoms.Except(list.FirstOrDefault().symptoms).ToList();

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
    public class Input
    {
        public List<string> positive;
        public List<string> negative;
    }
}