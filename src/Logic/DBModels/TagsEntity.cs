using Logic.Enums;

namespace Logic.DBModels
{
    [Dapper.Contrib.Extensions.Table("tags")]
    public class TagsEntity : DBEntity, Interfaces.IDBEntity
    {
        public string tag_name {get; set;}

    }
}
