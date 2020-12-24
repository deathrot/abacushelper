using Logic.Enums;

namespace Logic.DBModels
{
    [Dapper.Contrib.Extensions.Table("abacus_books_pages_questions")]
    public class TagsEntity : DBEntity
    {
        public string tag_name {get; set;}

    }
}
