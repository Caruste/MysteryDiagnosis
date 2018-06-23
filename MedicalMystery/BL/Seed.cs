using DAL.App.Interfaces;
using Domains;
using MedicalMystery.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalMystery.BL
{
    public class Seed
    {
        private readonly IConfiguration _configuration;

        public Seed(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Disease> buildData()
        {
            var csvLines = File.ReadAllLines(_configuration["Database"]);
            List<Disease> diseases = new List<Disease>();

            foreach (var csvLine in csvLines)
            {
                string[] temp = csvLine.Split(',');
                diseases.Add(new Disease()
                {
                    name = temp[0],
                    symptoms = allSymptoms(temp)
                });
            };
            return diseases;
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
