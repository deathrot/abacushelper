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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var q = (MultiplicationQuestion)obj;
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
