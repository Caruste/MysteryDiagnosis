using DAL.Interfaces.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.App.Interfaces.Interfaces
{
    public interface IDiseasesRepository: IRepository<Disease>
    {
        bool Exists(Disease disease);
        IEnumerable<Disease> AllWithSymptoms();
    }
}
