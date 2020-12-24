using System.Collections.Generic;
using System.Linq;
using QuestionType = Logic.Enums.QuestionType;

namespace Logic.Question
{
    public class BeedQuestion : IQuestion
    {
        public QuestionType QuestionType { get; } = QuestionType.Beed;

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
            if ( Numbers.Count == 0 || Numbers.Any(x => x.Number < 0))
            {
                return false;
            }

            return true;
        }
    }
}
