using System.Collections.Generic;

namespace Logic.ViewModels
{
    public class SettingVM : Entity
    {
                
        public string Name {get; set;}

        public string Value {get; set;}

        public Logic.Enums.DataType DataType { get; set; }

        public int SortOrder { get; set; }

    }
}