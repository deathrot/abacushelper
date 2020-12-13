using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.DBModels
{
    [Dapper.Contrib.Extensions.Table("abacus_books")]
    public class AbacusBooks : NamedDBEntity
    {        
        public string AbacusLevelId {get; set;}

    }
}
