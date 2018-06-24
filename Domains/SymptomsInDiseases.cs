using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains
{
    public class SymptomsInDiseases
    {
        public int SymptomsInDiseasesId { get; set; }

        [Key]
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }

        [Key]
        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
    }
}
