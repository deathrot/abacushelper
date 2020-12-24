using System;

namespace Logic.DBModels
{
    public class DBEntity
    {
        
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string id {get; set;}

        public DateTime modified_on {get; set;}
        
        public int sort_order {get; set;}

        public bool is_deleted {get; set;}
        
    }
}