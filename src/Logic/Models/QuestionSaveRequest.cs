using System.Collections.Generic;

namespace Logic.Models
{
    public class QuestionSaveRequest
    {
        public Logic.ViewModels.QuestionVM[] entitesToUpdate { get; set; }
        
        public Logic.ViewModels.QuestionVM[] entitesToDelete { get; set; }

        public Logic.ViewModels.QuestionVM[] entitesToInsert { get; set; }

        public bool IsEmpty{
            get
            {
                if ( entitesToDelete != null && entitesToDelete.Length > 0)
                    return false;
                    
                if ( entitesToInsert != null && entitesToInsert.Length > 0)
                    return false;
                    
                if ( entitesToUpdate != null && entitesToUpdate.Length > 0)
                    return false;

                return true;
            }
        }
    }
}