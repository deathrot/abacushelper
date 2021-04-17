using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using QuestionSubType = Logic.Enums.QuestionSubType;

namespace Logic.Question
{
    public class AvergaQuestion : IQuestion
    {
        public QuestionSubType QuestionSubType { get; } = QuestionSubType.Average;

        public List<SignedNumber> Numbers { get; set;  } = new List<SignedNumber>();

        public decimal Calculate()
        {
            decimal result = 0;
            foreach(var t in Numbers.OrderBy(x => x.SortOrder))
            {
                result += t.Number;
            }
            return result/Numbers.Count;
        }

        public bool IsValid()
        {
            if ( Numbers.Count > 0)
            {
                return true;
            }

            return false;
        }

        // override object.Equals
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

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return string.Join(",", Numbers.OrderBy(x => x.Number).Select(x => x.Number)).GetHashCode();
            /*int i = Int32.MaxValue;
            foreach(var n in Numbers.OrderBy(x => x.Number).Select(x => x.Number).GetHashCode())
            {
                i = i ^ n.Number.GetHashCode();    
            }

            return i;*/
        }

        public override string ToString()
        {
            return Constants.Constants.OutputToString(this);
        }
    }
}
