using DAL.Interfaces.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.App.Interfaces.Interfaces
{
    /// <summary>
    /// This interface is used to tell SymptomsRepository which methods should be used.
    /// It is also used for dependency injection.
    /// </summary>
    public interface ISymptomsRepository: IRepository<Symptom>
    {
        Symptom FindByName(string name);
        int SymptomsCount();
    }
}
