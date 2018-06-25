using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains
{
    /// <summary>
    /// This class is used to define the many to many model to the database. 
    /// This class is made because we need many to many table for Diseases and Symptoms. 
    /// </summary>
    public class SymptomsInDiseases
    {
        // ID of the SymptomsInDiseases, only used for the database. 
        [Key]
        public int SymptomsInDiseasesId { get; set; }

        // Foreign key for the symptom
        #region Symptom area
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }
        #endregion

        // Foreign key for the disease
        #region Disease area
        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
        #endregion
    }
}
