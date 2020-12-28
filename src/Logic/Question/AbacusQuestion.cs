using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using QuestionSubType = Logic.Enums.QuestionSubType;

namespace Logic.Question
{
    public class AddSubQuestion : IQuestion
    {
        public QuestionSubType QuestionType { get; } = QuestionSubType.AddSub;

        public List<SignedNumber> Numbers { get; set;  } = new List<SignedNumber>();

        public decimal Calculate()
        {
            decimal result = 0;
            foreach(var t in Numbers.OrderBy(x => x.SortOrder))
            {
                result += t.Number;
            }
            return result;
        }

        public bool IsValid()
        {
            if ( Numbers.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}
