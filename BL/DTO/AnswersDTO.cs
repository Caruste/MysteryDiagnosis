using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTO
{

    /// <summary>
    /// This class is used to get information from the front-end. 
    /// This is used to return the answers from the questioning part. 
    /// </summary>
    public class AnswersDTO
    {
        public List<string> positive;
        public List<string> negative;
    }
}
