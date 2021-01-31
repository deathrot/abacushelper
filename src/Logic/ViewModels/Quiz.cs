using System.Collections.Generic;

namespace Logic.ViewModels
{
    public class Quiz
    {
        public decimal MaxLevel {get; set;}

        public decimal MinLevel {get; set;}

        public QuizQuestion[] Questions {get; set;}
   }

    public class QuizQuestion
    {
        public Logic.Enums.QuestionType QuestionType {get; set;}

        public Logic.Enums.QuestionSubType QuestionSubType {get; set;}

        public int SortOrder {get; set;}

        public List<Logic.Question.IQuestion> Questions {get; set;} = new List<Question.IQuestion>();
    }
}