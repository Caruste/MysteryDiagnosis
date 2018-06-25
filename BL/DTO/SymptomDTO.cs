using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTO
{
    class SymptomDTO
    {
        public string name;

        public static SymptomDTO Transform(Symptom symptom)
        {
            return new SymptomDTO()
            {
                name = symptom.Name
            };
        }
    }
}
