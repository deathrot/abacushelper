using Logic.Interfaces;
using System.Threading.Tasks;

namespace Logic.Providers
{
    public class QuizProvider : IQuizProvider
    {
        
        public async Task<ViewModels.Quiz> GetQuiz(decimal minLevel, decimal maxLevel)
        {
            await Task.Delay(0);

            Engine.QuizEngine qe = new Engine.QuizEngine();
            return qe.CreateQuiz(minLevel, maxLevel);
        }


    }
}