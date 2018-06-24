﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains
{
    public class Disease
    {
        public int DiseaseId { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
        public List<SymptomsInDiseases> SymptomsInDiseases { get; set; } = new List<SymptomsInDiseases>();

        public virtual List<Symptom> Symptoms { get; set; } = new List<Symptom>();
        [NonSerialized]
        public List<string> SymptomString;
    }
}
