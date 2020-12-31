using System;

namespace Logic.ViewModels
{

    public class UserVM : Entity
    {

        public string UserEmail { get; set; }
        
        public string Password { get; set; }
        
        public DateTime LastLoginOn { get; set; }
                
        public DateTime LastLogOut { get; set; }
        
        public bool IsLockedOut { get; set; }
        
        public bool IsConfirmed { get; set; }
        
        public int NumberOfFailedPasswordAttempt { get; set; }

    }
}