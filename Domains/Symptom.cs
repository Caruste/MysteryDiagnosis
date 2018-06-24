using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains
{
    public class Symptom
    {
        [Key]
        public int SymptomId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        public List<SymptomsInDiseases> SymptomsInDiseases { get; set; } = new List<SymptomsInDiseases>();
    }
}
