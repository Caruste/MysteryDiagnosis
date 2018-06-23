using DAL.App.Interfaces;
using Domains;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace DAL.App.Database
{
    public class DbContext : IDataContext
    {
        private readonly IConfiguration _configuration;

        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Disease> All()
        {
            var csvLines = File.ReadAllLines(_configuration["Database"]);
            List<Disease> diseases = new List<Disease>();
            

            foreach (var csvLine in csvLines)
            {
                diseases.Add(createDiseaseFromString(csvLine));
            };
            return diseases;
        }

        private Disease createDiseaseFromString(string input)
        {
            string[] temp = input.Split(',');
            return new Disease()
            {
                name = temp[0],
                symptoms = allSymptoms(temp)
            };
        }

        private List<string> allSymptoms(string[] list)
        {
            List<string> tempList = new List<string>(list);
            tempList.RemoveAt(0);
            return trimList(tempList);
        }

        private List<string> trimList(List<string> list)
        {
            List<string> tempList = new List<string>();
            foreach (string item in list)
            {
                tempList.Add(item.Trim());
            }
            return tempList;
        }
    }
}
