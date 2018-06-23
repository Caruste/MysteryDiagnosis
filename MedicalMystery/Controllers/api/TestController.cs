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
#warning Not adding items to the database.
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
                if (input.positive.Count == 0)
                {
                    hasAll = diseases;
                }
                else
                {
                    foreach (Disease item in diseases)
                    {
                        if (ContainsAllItems(item.symptoms, input.positive)) hasAll.Add(item);
                    }
                }
            }

            List<Disease> final = new List<Disease>();

            if (input.negative != null)
            {
                if (input.negative.Count == 0)
                {
                    final = hasAll;
                }
                else
                {
                    foreach (Disease item in hasAll)
                    {
                        final = hasAll.Where(x => !input.negative.Any(t => input.negative.Contains(t))).ToList(); 
                    }   
                }
            }

            var list = final.OrderByDescending(x => x.symptoms.Count);
            Disease disease = new Disease();
            if (list.Count() <= 1) return Ok(list.FirstOrDefault());
            if (list.FirstOrDefault().symptoms.Count == input.positive.Count) disease = list.ElementAt(1);

#warning Must find diferences between the lists and use the element from that. If one has symptom#1 and another one has the same one then dont ask that. Must ask the one which both dont have.

            else disease = list.FirstOrDefault();

            foreach (var item in disease.symptoms)
            {
                if (!input.positive.Contains(item) && !input.negative.Contains(item)) return Ok(item);
            }

            return NotFound();
        }

        public static bool ContainsAllItems(List<string> a, List<string> b)
        {
            return !b.ConvertAll(x => x.ToLower()).Except(a.ConvertAll(x => x.ToLower())).Any();
        }
#warning Setup database now so it would be easier later!
#warning Must add a way to add String[] types to the database
#warning Must start a database
    }
    public class Input
    {
        public List<string> positive;
        public List<string> negative;
    }
}