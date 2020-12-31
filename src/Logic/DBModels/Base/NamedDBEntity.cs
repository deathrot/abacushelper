using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.DBModels
{
    public class NamedDBEntity : DBEntity
    {
        public string record_name {get; set;}
        public string record_description {get; set;}
    }
}
