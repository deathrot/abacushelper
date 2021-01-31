using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using QuestionSubType = Logic.Enums.QuestionSubType;

namespace Logic.Question
{
    public class MultiplicationQuestion : IQuestion
    {
        public QuestionSubType QuestionSubType { get; } = QuestionSubType.Multiplication;

        public List<SignedNumber> Numbers { get; set;  } = new List<SignedNumber>();

        public decimal Calculate()
        {
            decimal result = 1;
            foreach(var n in Numbers.OrderBy(x => x.SortOrder))
            {
                result *= n.Number;
            }
            return result;
        }

        public bool IsValid()
        {
            if ( Numbers.Count > 1)
            {
                return true;
            }

            return false;
        }
    }
}
