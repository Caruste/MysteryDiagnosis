using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains
{

    /// <summary>
    /// This class is used to define the model to the database. 
    /// </summary>
    public class Disease
    {
        // ID of the disease, only used for the database. 
        [Key]
        public int DiseaseId { get; set; }

        // Name of the Disease
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        // Many to many relationship table- This is used for include method.
        public List<SymptomsInDiseases> SymptomsInDiseases { get; set; } = new List<SymptomsInDiseases>();

        // Symptoms are used to make searching for symptoms easier.
        [NonSerialized]
        public List<Symptom> Symptoms;
    }
}
