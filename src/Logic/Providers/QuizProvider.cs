using Logic.Interfaces;
using System.Threading.Tasks;

namespace Logic.Providers
{
    public class QuizProvider : IQuizProvider
    {
        
        public async Task<ViewModels.Quiz> GetQuiz(decimal minLevel, decimal maxLevel)
        {
            await Task.Delay(0);

            var result = new ViewModels.Quiz();

            result.MaxLevel = maxLevel;
            result.MinLevel = minLevel;

            int questionSort = 0;
            while(result.Questions.Count < 10)
            {
                var asq = new Question.AddSubQuestion();

                var random = new System.Random();
                int totalNumbers = random.Next(2, 8);
                int sortOrder = 0;
                while (asq.Numbers.Count < totalNumbers)
                {
                    int number = random.Next(-10, 55);
                    asq.Numbers.Add(new Question.SignedNumber() { Number = number, SortOrder = sortOrder });
                    sortOrder++;
                }

                result.Questions.Add(new ViewModels.QuizQuestion { Question = asq, SortOrder = questionSort, QuestionType = Enums.QuestionType.Math, QuestionSubType = asq.QuestionSubType });
                questionSort++;
            }


            /*var asq = new Question.AddSubQuestion();
            asq.Numbers.Add(new Question.SignedNumber(){ SortOrder = 0, Number = 10});
            asq.Numbers.Add(new Question.SignedNumber(){ SortOrder = 1, Number = 25});
            asq.Numbers.Add(new Question.SignedNumber(){ SortOrder = 2, Number = -15});
            asq.Numbers.Add(new Question.SignedNumber(){ SortOrder = 3, Number = 14});
            asq.Numbers.Add(new Question.SignedNumber(){ SortOrder = 4, Number = 13});

            var mq = new Question.MultiplicationQuestion();
            mq.Numbers.Add(new Question.SignedNumber(){ SortOrder = 0, Number = 5});
            mq.Numbers.Add(new Question.SignedNumber(){ SortOrder = 1, Number = 6});

            var sq = new Question.SequentialQuestion();
            sq.Numbers.Add(new Question.SignedNumber(){ SortOrder = 0, Number = 5});

            result.Questions.Add(new ViewModels.QuizQuestion() { SortOrder = 0, Question = asq, QuestionType = Enums.QuestionType.Math, QuestionSubType = asq.QuestionSubType});
            result.Questions.Add(new ViewModels.QuizQuestion() { SortOrder = 1, Question = mq, QuestionType = Enums.QuestionType.Math, QuestionSubType = mq.QuestionSubType});
            result.Questions.Add(new ViewModels.QuizQuestion() { SortOrder = 2, Question = sq, QuestionType = Enums.QuestionType.Math, QuestionSubType = sq.QuestionSubType});*/

            return result;
        }


    }
}