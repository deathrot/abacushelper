using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using QuestionSubType = Logic.Enums.QuestionSubType;

namespace Logic.Question
{
    public class SimpleDivisionQuestion : IQuestion
    {
        public QuestionSubType QuestionSubType { get; } = QuestionSubType.SimpleDivision;

        public List<SignedNumber> Numbers { get; set;  } = new List<SignedNumber>();

        public bool AllowRemainder {get; set;}

        public decimal Calculate()
        {
            SignedNumber numerator = Numbers.First(x => x.SortOrder == 1);
            SignedNumber denominator = Numbers.First(x => x.SortOrder == 2);
            
            return numerator.Number/denominator.Number;
        }

        public bool IsValid()
        {
            if ( Numbers.Count == 2 && Numbers.First(x => x.SortOrder == 2).Number != 0)
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

            var q = (SimpleDivisionQuestion)obj;
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

        // override object.GetHashCode
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
