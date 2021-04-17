using Logic.Interfaces;
using System.Threading.Tasks;

namespace Logic.Engine
{
    public class QuizEngine
    {
        QuestionCylinders.IQuestionCylinder[] cylinders = new QuestionCylinders.IQuestionCylinder[3]{
            new QuestionCylinders.AddSubQuestionCylinder(),
            new QuestionCylinders.MultiplicationCylinder(),
            new QuestionCylinders.SequentialCylinder()
        };
        
        public ViewModels.Quiz CreateQuiz(decimal minLevel, decimal maxLevel)
        {
            ViewModels.Quiz q = new ViewModels.Quiz();
            q.MinLevel = minLevel;
            q.MaxLevel = maxLevel;
            
            decimal currentLevel = minLevel;

            int totalQuestions = 0;
            while(totalQuestions < 10)
            {
                int d = PeanutButter.RandomGenerators.RandomValueGen.GetRandomInt(0, 2);
                var cylinder = cylinders[d];

                var question = cylinder.Fire(currentLevel);
                var qq = new ViewModels.QuizQuestion(){
                    Question = question,
                    SortOrder = totalQuestions
                };
                q.Questions.Add(qq);
                totalQuestions++;
                currentLevel = PeanutButter.RandomGenerators.RandomValueGen.GetRandomDecimal(currentLevel, maxLevel);
            }

            return q;
        }

    }
}