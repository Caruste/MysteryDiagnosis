using DAL.Interfaces.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.App.Interfaces.Interfaces
{
    /// <summary>
    /// This interface is used to tell SymptomsInDiseaseRepository which methods should be used.
    /// It is also used for dependency injection.
    /// </summary>
    public interface ISymptomsInDiseaseRepository : IRepository<SymptomsInDiseases>
    {
        IEnumerable<SymptomsInDiseases> AllReferencesToSymptoms();
    }
}
