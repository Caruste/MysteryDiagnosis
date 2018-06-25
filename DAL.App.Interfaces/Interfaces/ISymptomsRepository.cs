using DAL.Interfaces.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.App.Interfaces.Interfaces
{
    public interface ISymptomsRepository: IRepository<Symptom>
    {
        Symptom FindByName(string name);
        int SymptomsCount();
    }
}
