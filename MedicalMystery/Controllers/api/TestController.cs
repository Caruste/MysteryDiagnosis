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
            if (list.Count == 0)return NotFound();
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

        public static bool ContainsAllItems(List<string> a, List<string> b)
        {
            return !b.ConvertAll(x => x.ToLower()).Except(a.ConvertAll(x => x.ToLower())).Any();
        }
#warning Setup database now so it would be easier later!
#warning Must add a way to add String[] types to the database
#warning Must start a database
    }
}