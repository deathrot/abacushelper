using System;

namespace Logic.DBModels
{
    public class DBEntity
    {
        
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string Id {get; set;}

        public DateTime ModifiedOn {get; set;}
        
        public int SortOrder {get; set;}
        
    }
}