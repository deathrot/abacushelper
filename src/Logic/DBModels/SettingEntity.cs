using Logic.Enums;

namespace Logic.DBModels
{
    [Dapper.Contrib.Extensions.Table("abacus_books_pages_questions")]
    public class SettingEntity : DBEntity
    {

        public string setting_name {get; set;}

        public Enums.DataType setting_data_type {get; set;}

        public string setting_value {get; set;}

    }
}
