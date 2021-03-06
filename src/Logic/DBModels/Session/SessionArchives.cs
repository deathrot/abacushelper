using System;

namespace  Logic.DBModels
{
    
    [Dapper.Contrib.Extensions.Table("session_archives")]
    public class SessionArchiveEntity : DBEntity, Interfaces.IDBEntity
    {
       
        public string user_id {get; set;}
	    
        public DateTime login_time {get; set;}
    	
        public DateTime last_activity_time {get; set;}
	    
        public DateTime log_out_time {get; set;}

        public string session_token { get; set; }

    }
}