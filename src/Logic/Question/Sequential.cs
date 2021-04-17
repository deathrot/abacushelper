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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var q = (AddSubQuestion)obj;
            if ( q.Numbers.Count != this.Numbers.Count || q.Calculate() != this.Calculate())
            {
                return false;
            }

            foreach(var t in q.Numbers)
            {
                if ( !this.Numbers.Any(x => x.Number == t.Number))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return string.Join(",", Numbers.OrderBy(x => x.Number).Select(x => x.Number)).GetHashCode();
        }

        public override string ToString()
        {
            return Constants.Constants.OutputToString(this);
        }
    }
}
