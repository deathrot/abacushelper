using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Question
{
    [Dapper.Contrib.Extensions.Table("abacus_books_pages_questions")]
    public class QuestionEntity
    {

        [Dapper.Contrib.Extensions.ExplicitKey]
        public string Id {get; set;}
    AbacusBooksPagesId NVARCHAR(256),
	Question JSON NOT NULL,
    SortOrder integer,
	ModifiedOn datetime not null,


    }
}
