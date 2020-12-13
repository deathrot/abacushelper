using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.DBModels
{
    [Dapper.Contrib.Extensions.Table("abacus_book_pages")]
    public class AbacusBookPage : NamedDBEntity
    {        
        public string AbacusBookId {get; set;}

    }
}
