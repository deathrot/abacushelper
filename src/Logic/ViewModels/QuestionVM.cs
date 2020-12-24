using System.Collections.Generic;

namespace Logic.ViewModels
{
    public class QuestionVM : Entity
    {
        
        public Enums.Severity severity {get; set;}
        
        public int LevelId {get; set;}

        public int SubLevelId{ get; set;} 
        
        public string QuestionJSON {get; set;}
        
        public List<string> Tags {get; set;}

        public int Sort {get; set;}

        public string Name {get; set;}

        public string Description {get; set;}

    }
}