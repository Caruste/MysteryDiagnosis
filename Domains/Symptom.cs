using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains
{
    /// <summary>
    /// This class is used to define the model to the database. 
    /// </summary>
    public class Symptom
    {
        // ID of the disease, only used for the database. 
        [Key]
        public int SymptomId { get; set; }

        // Name of the Symptom
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        // Many to many relationship table- This is used for include method.
        public List<SymptomsInDiseases> SymptomsInDiseases { get; set; } = new List<SymptomsInDiseases>();

        #region Constructors
        public Symptom()
        {
        }

        public Symptom(string name)
        {
            Name = name;
        }
        #endregion
    }
}
