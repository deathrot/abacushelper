using System.Threading.Tasks;

namespace Logic.Interfaces
{
    [Logic.ServicesExtensions.Service]
    public interface IQuizProvider
    {
        
        Task<ViewModels.Quiz> GetQuiz(decimal minLevel, decimal maxLevel);

    }
}