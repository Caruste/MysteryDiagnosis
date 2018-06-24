using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains
{
    public class Symptom
    {
        public int SymptomId { get; set; }

        [Required]
        [MaxLength(32)]
        public string name { get; set; }
        public List<SymptomsInDiseases> SymptomsInDiseases { get; set; } = new List<SymptomsInDiseases>();
    }
}
