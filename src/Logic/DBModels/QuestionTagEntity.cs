using Logic.Enums;

namespace Logic.DBModels
{
    [Dapper.Contrib.Extensions.Table("question_tags")]
    public class QuestionTagEntity : DBEntity, Interfaces.IDBEntity
    {
        public string tag_id {get; set;}

        public string question_id {get; set;}
        
    }
}
