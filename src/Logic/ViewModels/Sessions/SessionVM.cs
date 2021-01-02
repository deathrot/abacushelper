using System;

namespace Logic.ViewModels
{

    public class SessionVM : Entity
    {

        public string UserId {get; set;}

        public string SessionToken { get; set; }

        public DateTime LoginTime {get; set;}
    	
        public DateTime LastActivityTime {get; set;}
	    
        public DateTime NextLoginTimeout {get; set;}

        public Enums.LoginMethod LoginMethod {get; set;}

    }
}