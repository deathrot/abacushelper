using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.DBModels
{
    [Dapper.Contrib.Extensions.Table("abacus_books_pages_questions")]
    public class QuestionEntity : DBEntity
    {
        public string AbacusBooksPagesId {get; set;}
        public string Question {get; set;}
    }
}
