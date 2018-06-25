using DAL.Interfaces.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.App.Interfaces.Interfaces
{
    /// <summary>
    /// This interface is used to tell DiseasesRepository which methods should be used.
    /// It is also used for dependency injection.
    /// </summary>
    public interface IDiseasesRepository: IRepository<Disease>
    {
        bool Exists(Disease disease);
        IEnumerable<Disease> AllWithSymptoms();
    }
}
