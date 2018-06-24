using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains
{
    public class SymptomsInDiseases
    {
        [Key]
        public int SymptomsInDiseasesId { get; set; }

        #region Symptom area
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }
        #endregion


        #region Disease area
        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
        #endregion
    }
}
