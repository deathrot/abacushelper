using Logic.Enums;

namespace Logic.DBModels
{
    [Dapper.Contrib.Extensions.Table("questions")]
    public class QuestionEntity : NamedDBEntity, Interfaces.IDBEntity
    {
        public Severity severity {get; set;}

        public Enums.QuestionType question_type {get; set;}

        public Enums.QuestionSubType question_sub_type {get; set;}

        public int level {get; set;}

        public int sub_level {get; set;}

        public string question {get; set;}
    }
}
