using BL.DTO;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{
    public interface ISymptomService
    {
        int UniqueSymptomsCount();
        IEnumerable<string> TopThreeSymptoms();
        IEnumerable<string> CheckSymptoms(List<string> symptoms);
        object GetNextQuestion(AnswersDTO input);
    }
}
