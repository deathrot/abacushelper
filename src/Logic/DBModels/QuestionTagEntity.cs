using Logic.Enums;

namespace Logic.DBModels
{
    [Dapper.Contrib.Extensions.Table("abacus_books_pages_questions")]
    public class QuestionTagEntity : DBEntity
    {
        public string tag_id {get; set;}

        public string question_id {get; set;}
        
    }
}
