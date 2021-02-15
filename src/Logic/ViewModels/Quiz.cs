using System.Collections.Generic;

namespace Logic.ViewModels
{
    public class Quiz
    {
        public decimal MaxLevel {get; set;}

        public decimal MinLevel {get; set;}

        public List<QuizQuestion> Questions {get; set;} = new List<QuizQuestion>();
   }

    public class QuizQuestion
    {
        public decimal Level {get; set;}

        public Logic.Enums.QuestionType QuestionType {get; set;}

        public Logic.Enums.QuestionSubType QuestionSubType {get; set;}

        public int SortOrder {get; set;}

        public Logic.Question.IQuestion Question {get; set;}
    }
}