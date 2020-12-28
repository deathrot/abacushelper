using System.Collections.Generic;

namespace Logic.ViewModels
{
    public class QuestionVM : Entity
    {
        
        public string Severity {get; set;}
        
        public int Level {get; set;}

        public int SubLevel { get; set;} 
        
        public string QuestionJSON {get; set;}

        public string QuestionType { get; set; }

        public List<string> Tags { get; set; } = new List<string>();

        public int SortOrder {get; set;}

        public string Name {get; set;}

        public string Description {get; set;}

    }
}