using System;

namespace Logic.Models
{
    public class CreateUserResult
    {
        public bool Success {get; set;}

        public Enums.CreateUserReason Reason {get; set;}

        public string UserId {get; set;}
    }
}