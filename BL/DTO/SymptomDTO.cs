using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTO
{
    /// <summary>
    /// This method is used to transfer symptoms from backend to frontend.
    /// This is simply the symptoms name
    /// For more complicated frontend ID's should be added.
    /// </summary>
    class SymptomDTO
    {
        public string name;


        /// <summary>
        /// This method is used to transform a Symptom object to SymptomDTO object
        /// </summary>
        /// <param name="symptom">Symptom which we wish to transform</param>
        /// <returns>SymptomDTO which was transformed from input</returns>
        public static SymptomDTO Transform(Symptom symptom)
        {
            return new SymptomDTO()
            {
                name = symptom.Name
            };
        }
    }
}
