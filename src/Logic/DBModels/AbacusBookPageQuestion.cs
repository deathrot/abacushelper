using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.DBModels
{
    [Dapper.Contrib.Extensions.Table("abacus_book_page_questions")]
    public class AbacusBookPageQuestion : NamedDBEntity
    {        
        public string AbacusBookPageId {get; set;}

    }
}
