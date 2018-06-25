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

    /// <summary>
    /// This controller is used to return anything symptoms related
    /// To access is you must use .../api/symptoms/{method}
    /// </summary>
    [Produces("application/json")]
    [Route("api/Symptoms")]
    public class SymptomsController : Controller
    {
        private readonly ISymptomService _symptomService;
        public SymptomsController(ISymptomService symptomService)
        {
            _symptomService = symptomService;
        }

        /// <summary>
        /// This method is used to get top three symptoms by their frequency in diseases.
        /// </summary>
        /// <returns>IEnumerable of the top three symptoms in diseases and status code.</returns>
        [HttpGet("Top")]
        public IActionResult topSymptoms()
        {
            return Ok(_symptomService.TopThreeSymptoms());
        }

        /// <summary>
        /// This method is used to get the amount of unique symptoms in the database. 
        /// </summary>
        /// <returns>Number of symptoms in the database as an integer</returns>
        [HttpGet("Amount")]
        public IActionResult symptomCount()
        {
            return Ok(_symptomService.UniqueSymptomsCount());
        }

        /// <summary>
        /// This method is used to get the next question to ask from the user. 
        /// It checks what has been answered as a positive and negative and
        ///     through that process it decides the next question.
        /// The response can be a string or an object Disease. 
        /// If it's a disease then it is the diagnosis.
        /// </summary>
        /// <param name="input">All the answers from the user and whether they are positive or not</param>
        /// <returns>Returns a string or a disease object. If its a disease then it is the diagnosis.</returns>
        [HttpPost("Question")]
        public IActionResult GetNextQuestion([FromBody] AnswersDTO input)
        {
            return Ok(_symptomService.GetNextQuestion(input));
        }

        /// <summary>
        /// This method is used to get all of the diseases that match with the input symptoms
        /// </summary>
        /// <param name="list">Symptoms against which we are comparing the diseases.</param>
        /// <returns>List of Diseases names as string list that match the symptoms.</returns>
        [HttpPost]
        public IActionResult CheckSymptoms([FromBody] List<string> list)
        {
            return Ok(_symptomService.CheckSymptoms(list));
        }
    }
}