using DAL.Interfaces.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.App.Interfaces.Interfaces
{
    public interface ISymptomsInDiseaseRepository : IRepository<SymptomsInDiseases>
    {
        IEnumerable<SymptomsInDiseases> AllSymptomIds();
    }
}
