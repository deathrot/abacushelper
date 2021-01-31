using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using QuestionSubType = Logic.Enums.QuestionSubType;

namespace Logic.Question
{
    public class SequentialQuestion : IQuestion
    {
        public QuestionSubType QuestionSubType { get; } = QuestionSubType.Sequentials;

        public List<SignedNumber> Numbers { get; set;  } = new List<SignedNumber>();

        public decimal Calculate()
        {
            decimal result = 0;
            for(int i = 1; i<=Numbers[0].Number;i++)
            {
                result += i;
            }
            foreach(var t in Numbers.OrderBy(x => x.SortOrder))
            {
                result += t.Number;
            }
            return result;
        }

        public bool IsValid()
        {
            if ( Numbers.Count == 1)
            {
                return true;
            }

            return false;
        }
    }
}
