using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{
    public interface ISymptomsInDiseaseService
    {
        SymptomsInDiseases createNew(Symptom symptom, Disease disease, int i);
    }
}
