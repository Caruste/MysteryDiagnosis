using BL.DTO;
using BL.Interfaces;
using DAL.App.Interfaces.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Services
{
    public class SymptomService : ISymptomService
    {
        private readonly ISymptomsRepository _symptomRepo;
        private readonly ISymptomsInDiseaseRepository _sidRepository;
        private readonly IDiseasesService _diseasesService;

        public SymptomService(ISymptomsRepository symptomsRepo, ISymptomsInDiseaseRepository symptomsInDiseaseRepository, IDiseasesService diseasesService)
        {
            _symptomRepo = symptomsRepo;
            _sidRepository = symptomsInDiseaseRepository;
            _diseasesService = diseasesService;
        }

        public IEnumerable<string> CheckSymptoms(List<string> symptoms)
        {
            IEnumerable<DiseaseDTO> diseases = _diseasesService.AllWithSymptoms();
            if (symptoms.Count == 0 || diseases == null) return null;

            return diseases
                .Where(x => ContainsAllItems(x.symptoms, symptoms))
                .Select(x => x.name);
        }

        public IEnumerable<string> TopThreeSymptoms()
        {

            IEnumerable<SymptomsInDiseases> symptomsInDiseases = _sidRepository.AllSymptomIds();
            if (symptomsInDiseases == null) return null;

            // This dictionary will be used to count the symptoms frequency.
            Dictionary<string, int> symptoms = new Dictionary<string, int>();

            // Looping through symptoms and adding them to the dictionary.
            // If the symptom is already in the dictionary then we will 
            // Increment the value up by 1
            foreach (var item in symptomsInDiseases)
            {
                if (symptoms.ContainsKey(item.Symptom.Name)) symptoms[item.Symptom.Name]++;
                else symptoms.Add(item.Symptom.Name, 0);
            }

            /*  Returning out the symptoms. 
             *  First we will order the dictonary from highest frequency to the lowest.
             *  Then we will order the dictionary alphabetically and select only strings
             *  Then we will take the first 3 elements and output as a it as a List.
             */
            return symptoms
                        .OrderByDescending(x => x.Value)
                        .ThenBy(x => x.Key)
                        .Select(x => x.Key.ToString())
                        .Take(3);
        }

        public int UniqueSymptomsCount()
        {
            return _symptomRepo.SymptomsCount();
        }

        public object GetNextQuestion(AnswersDTO input)
        {
            IEnumerable<DiseaseDTO> diseases = _diseasesService.AllWithSymptoms();

            if (diseases.Count() == 0 || diseases == null || input.positive == null || input.negative == null) return null;

            /*  First .Where statement checks if a given disease contains all of the inputs that the user
            *   has said that he/she has. 
            *   Second .Where statement checks if a given disease doesnt contain any of the input.negative that
            *   the user has said that he/she doesnt have
            *   Intersect gets the common parts of two lists. If there are any then that Disease will be dismissed!
            *   Then we order it by Sympotms count. We want the lower count diseases to be infront for faster searching
             */
            IEnumerable<DiseaseDTO> InputEvaluated = diseases
                            .Where(x => ContainsAllItems(x.symptoms, input.positive))
                            .Where(x => !x.symptoms.Intersect(input.negative).Any())
                            .OrderBy(x => x.symptoms.Count());

            // This returns an anonymous object back, which is used by front-end to detect final answer.
            if (InputEvaluated.Count() == 1) return (new
            {
                InputEvaluated.FirstOrDefault().name
            });

            // Here we take the first and second diseases symptoms, find the differences and output them
            // This is done so that with each question at-least one disease would be eliminated (not always though)
            IEnumerable<string> symptomList = InputEvaluated
                    .ElementAt(1)
                    .symptoms
                    .Except(InputEvaluated.FirstOrDefault().symptoms);

            // Return the next question. This is checked against what has already been asked to make sure we dont ask same question twice.
            foreach (var item in symptomList) if (!input.positive.Contains(item) && !input.negative.Contains(item)) return item;

            return null;
        }

        public static bool ContainsAllItems(IEnumerable<string> a, IEnumerable<string> b)
        {
            /*  First converting everything to lowercase to avoid false negative
             *  Using except to filter out everything from the List b (input-positive)
             *  If there is anything left in the B then false will be returned
             *  If Everything is removed then true is returned.
             */
            return !b.Select(x => x.ToLower())
                        .Except(a.Select(x => x.ToLower()))
                        .Any();
        }
    }
}
