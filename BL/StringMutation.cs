using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BL
{
    public static class StringMutation
    {
        public static Disease DiseaseFromString(string input)
        {
            string[] temp = Regex.Split(input, ", ");
            return new Disease()
            {
                Name = temp[0].Trim(),
                Symptoms = SymptomsFromString(allSymptoms(temp))
            };
        }

        private static List<string> allSymptoms(string[] list)
        {
            List<string> tempList = new List<string>(list);
            tempList.RemoveAt(0);
            return trimList(tempList);
        }

        private static List<string> trimList(List<string> list)
        {
            List<string> tempList = new List<string>();
            foreach (string item in list)
            {
                tempList.Add(item.Trim());
            }
            return tempList;
        }

        private static List<Symptom> SymptomsFromString(List<string> input)
        {
            List<Symptom> symptoms = new List<Symptom>();
            foreach (var item in input)
            {
                symptoms.Add(new Symptom()
                {
                    Name = item
                });
            }
            return symptoms;
        }
    }
}
