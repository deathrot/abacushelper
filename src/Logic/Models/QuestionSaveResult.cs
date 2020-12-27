using System.Collections.Generic;

namespace Logic.Models
{
    public class QuestionSaveResult
    {
        public int T {get; set;}
        public Logic.ViewModels.QuestionVM[] entitesToUpdate { get; set; }
        
        public Logic.ViewModels.QuestionVM[] entitesToDelete { get; set; }
        public Logic.ViewModels.QuestionVM[] entitesToInsert { get; set; }
    }
}