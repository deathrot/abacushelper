namespace Logic.DBModels
{
    [Dapper.Contrib.Extensions.Table("settings")]
    public class SettingEntity : DBEntity, Interfaces.IDBEntity
    {

        public string setting_name {get; set;}

        public Enums.DataType setting_data_type {get; set;}

        public string setting_value {get; set;}
        
        public int sort_order {get; set;}

    }
}
