using BL.DTO;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{
    public interface IDiseasesService
    {
        void AddAll(List<string> list);

        List<Disease> AllWithSymptoms();
        IEnumerable<Disease> All();
    }
}
