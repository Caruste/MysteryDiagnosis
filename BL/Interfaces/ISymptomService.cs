using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{
    public interface ISymptomService
    {
        void AddSymptom(Symptom symptom);
        Symptom FindByName(Symptom symptom);
        Symptom FindByName(string name);
        int UniqueSymptomsCount();
        IEnumerable<Symptom> AllSymptoms();
        IEnumerable<string> TopThreeSymptoms();
    }
}
