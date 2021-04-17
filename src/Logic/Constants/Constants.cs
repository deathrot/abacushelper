using System;
using System.Linq;
using System.Text;

namespace Logic.Constants
{
    public abstract class Constants
    {

        public const string TOKEN_TEXT = "AbacusHelper2020_KMAN_SWITCH_SORRY";

        public const int SESSION_TIMEOUT_IN_SECONDS = 15 * 60;

        public const int MAX_CYLINDER_TRIES = 1000;

        public static string OutputToString(Question.IQuestion question)
        {
            StringBuilder sb = new StringBuilder();

            if (question.QuestionSubType == Enums.QuestionSubType.AddSub)
            {

                foreach (var n in question.Numbers.OrderBy(x => x.SortOrder))
                {
                    char sign = n.Number >= 0 ? '+' : '-';
                    sb.Append($"{n.Number}{sign}");
                }
            }
            else if (question.QuestionSubType == Enums.QuestionSubType.Multiplication)
            {
                sb.Append(string.Join(" X ", question.Numbers.OrderBy(x => x.SortOrder).Select(x => x.Number)));
            }
            else if (question.QuestionSubType == Enums.QuestionSubType.SimpleDivision)
            {
                sb.Append(string.Join(" / ", question.Numbers.OrderBy(x => x.SortOrder).Select(x => x.Number)));
            }
            else if (question.QuestionSubType == Enums.QuestionSubType.Sequentials)
            {
                bool first = true;
                for (int i = 1; i <= question.Numbers[0].Number; i++)
                {
                    if (first)
                    {
                        sb.Append(i);
                        first = false;
                        continue;
                    }

                    sb.Append($" + {i}");
                }
            }

            sb.Append($"= {question.Calculate()}");

            return sb.ToString();
        }


    }
}