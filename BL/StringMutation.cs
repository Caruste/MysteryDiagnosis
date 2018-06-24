using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BL
{
    /// <summary>
    /// This class is used to take a string and split it up at commas.
    /// </summary>
    public static class StringMutation
    {
        /// <summary>
        /// This method splits up the string and uses other methods 
        /// to clean the string up, make them into symptoms and add them to
        /// the disease.
        /// </summary>
        /// <param name="input">String which contains the Disease name and symptoms</param>
        /// <returns>Disease which is created by using the input string</returns>
        public static Disease DiseaseFromString(string input)
        {
            /*  Comma is used to split instead of Regex.Split(', ') because
            *   this way we can trim off any whitespace at the end and beginning
            *   and we can add to the list without using spaces.
            */
            string[] strings = input.Split(',');
            return new Disease()
            {
                Name = strings[0].Trim(),
                // First it takes all of the symptoms and trims them from whitespace
                // Then it selects all the elements and creates a symptom from each one of them.
                Symptoms = allSymptoms(strings)
                                .Select(x => new Symptom(x))
                                .ToList()
            };
        }

        /// <summary>
        /// This method is used to remove the first element of the list
        /// which must contain the name of the Disease and then it sends
        /// the list forward to get it trimmed.
        /// </summary>
        /// <param name="list">List which we are trimming</param>
        /// <returns>List<string> which contains trimmed strings and is ready to be assigned as symptoms</returns>
        private static List<string> allSymptoms(string[] list)
        {
            return list.Skip(1).Select(x => x.Trim()).ToList();
        }
    }
}
