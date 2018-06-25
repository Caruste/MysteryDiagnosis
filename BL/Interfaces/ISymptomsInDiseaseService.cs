using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{
    /// <summary>
    /// This interface is used for dependency injection and to say
    /// which methods should SymptomsInDiseaseService fulfill
    /// </summary>
    public interface ISymptomsInDiseaseService
    {
        SymptomsInDiseases createNew(Symptom symptom, Disease disease);
    }
}
