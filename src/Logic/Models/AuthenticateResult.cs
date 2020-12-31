using System;

namespace Logic.Models
{
    public class AuthenticateResult : DBModels.SessionsEntity
    {
        public bool password_check_passed {get; set;}

        public bool user_is_deleted { get; set; }

        public bool user_is_locked {get; set; }
        
        public Enums.AuthenticateResultType ResultType {
            get
            {
                if ( !password_check_passed){
                    return Enums.AuthenticateResultType.PasswordDoesNotMatch;
                }
                if ( is_deleted){
                    return Enums.AuthenticateResultType.UserIsDeleted;
                }
                if ( user_is_locked){
                    return Enums.AuthenticateResultType.UserIsLockedOut;
                }
                if (string.IsNullOrEmpty(userid)){
                    return Enums.AuthenticateResultType.UserDoesNotExists;
                }

                return (!string.IsNullOrEmpty(id) ? Enums.AuthenticateResultType.Success : Enums.AuthenticateResultType.UnknownError);
            }
        }
    }
}