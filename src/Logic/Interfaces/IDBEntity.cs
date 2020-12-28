using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Interfaces
{
    public interface IDBEntity
    {

        public string id { get; set; }

        DateTime modified_on { get; set; }

        bool is_deleted { get; set; }

    }
}
