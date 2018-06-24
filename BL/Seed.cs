//using Domains;
//using Microsoft.Extensions.Configuration;
//using System.Collections.Generic;
//using System.IO;

//namespace MedicalMystery.BL
//{
//    public class Seed
//    {
//        private readonly IConfiguration _configuration;

//        public Seed(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public List<Disease> buildData()
//        {
//            var csvLines = File.ReadAllLines(_configuration["Database"]);
//            List<Disease> diseases = new List<Disease>();

//            foreach (var csvLine in csvLines)
//            {
//                string[] temp = csvLine.Split(',');
//                diseases.Add(new Disease()
//                {
//                    Name = temp[0],
//                    SymptomString = allSymptoms(temp)
//                });
//            };
//            return diseases;
//        }
//        private List<string> allSymptoms(string[] list)
//        {
//            List<string> tempList = new List<string>(list);
//            tempList.RemoveAt(0);

//            foreach (var item in trimList(tempList))
//            {

//            }

//            return trimList(tempList);
//        }

//        private List<string> trimList(List<string> list)
//        {
//            List<string> tempList = new List<string>();
//            foreach (string item in list)
//            {
//                tempList.Add(item.Trim());
//            }
//            return tempList;
//        }
//    }
//}
