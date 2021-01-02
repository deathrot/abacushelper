using System;

namespace Logic.Models
{
    public class AuthenticateResult : Interfaces.IDBEntity
    {
        public bool password_check_passed {get; set;}

        public bool is_deleted { get; set; }

        public bool is_locked_out { get; set; }

        public string id { get; set; }

        public DateTime modified_on { get; set; }

        public Enums.AuthenticateResultType ResultType {
            get
            {
                if ( !password_check_passed){
                    return Enums.AuthenticateResultType.PasswordDoesNotMatch;
                }
                if ( is_deleted){
                    return Enums.AuthenticateResultType.UserIsDeleted;
                }
                if ( is_locked_out){
                    return Enums.AuthenticateResultType.UserIsLockedOut;
                }
                if (string.IsNullOrEmpty(id)){
                    return Enums.AuthenticateResultType.UserDoesNotExists;
                }

                return (!string.IsNullOrEmpty(id) ? Enums.AuthenticateResultType.Success : Enums.AuthenticateResultType.UnknownError);
            }
        }
    }
}