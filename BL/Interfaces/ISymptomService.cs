using BL.DTO;
using Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{
    /// <summary>
    /// This interface is used for dependency injection and to say
    /// which methods should SymptomService fulfill
    /// </summary>
    public interface ISymptomService
    {
        int UniqueSymptomsCount();
        IEnumerable<string> TopThreeSymptoms();
        IEnumerable<string> CheckSymptoms(List<string> symptoms);
        object GetNextQuestion(AnswersDTO input);
    }
}
