using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains
{
    public class Disease
    {
        [Key]
        public int DiseaseId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        public List<SymptomsInDiseases> SymptomsInDiseases { get; set; } = new List<SymptomsInDiseases>();

        [NonSerialized]
        public List<Symptom> Symptoms;
    }
}
