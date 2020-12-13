using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.DBModels
{
    public class NamedDBEntity : DBEntity
    {
        public string RecordName {get; set;}
        public string RecordDescription {get; set;}
    }
}
