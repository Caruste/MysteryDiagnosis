using BL.DTO;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{

    /// <summary>
    /// This interface is used for dependency injection and to say
    /// which methods should DiseasesService fulfill
    /// </summary>
    public interface IDiseasesService
    {
        void AddAll(List<string> list);

        IEnumerable<DiseaseDTO> AllWithSymptoms();
        IEnumerable<DiseaseDTO> TopThreeBySymptomsCount();
        void ClearDiseasesAndSymptomsDBFORTESTING();
    }
}
