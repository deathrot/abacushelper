using System;

namespace Logic.ViewModels
{

    public class SessionVM : Entity
    {

        public string UserId {get; set;}

        public string DisplayName { get; set; }

        public string EmailAddress { get; set; }

        public string SessionToken { get; set; }

        public DateTime LoginTime {get; set;}
    	
        public DateTime LastActivityTime {get; set;}
	    
        public DateTime NextLoginTimeout {get; set;}

        public Enums.LoginMethod LoginMethod {get; set;}

        public string AdditionalSessionData { get; set;  }

   
        public override int GetHashCode()
        {
            return this.SessionToken.GetHashCode() ^ this.UserId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var session = (SessionVM)obj;

                if (this.UserId == session.UserId && string.Compare(this.SessionToken, session.SessionToken) == 0 &&
                    this.LoginMethod == session.LoginMethod)
                {
                    return true;
                }
            }

            return false;
        }

    }
}