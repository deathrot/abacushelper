using Logic.Enums;

namespace Logic.DBModels
{
    [Dapper.Contrib.Extensions.Table("abacus_books_pages_questions")]
    public class QuestionEntity : NamedDBEntity
    {
        public Severity severity {get; set;}

        public Enums.QuestionType question_type {get; set;}

        public int level_id {get; set;}

        public int sub_level_id {get; set;}

        public string question {get; set;}
    }
}
