
namespace Logic.Models
{
    public class QuestionSaveResult
    {
        public Logic.ViewModels.QuestionVM[] entitesToUpdate { get; set; }
        public Logic.ViewModels.QuestionVM[] entitesToDelete { get; set; }
        public Logic.ViewModels.QuestionVM[] entitesToInsert { get; set; }
    }
}