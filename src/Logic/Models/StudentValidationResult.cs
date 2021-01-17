using System;

namespace Logic.Validators
{
    public class StudentValidationResult
    {
        
        public bool ValidationSuccess {get; set;}

        public bool EmailError {get; set;}

        public bool DisplayNameError {get; set;}

        public bool NameError {get; set;}

        public bool PasswordError {get; set;}

        public bool EmailAvailiability {get; set;}

        public bool DisplayNameAvaliability {get; set;}

        public bool HasError {
            get
            {
                return EmailError || DisplayNameError || PasswordError || NameError || !EmailAvailiability || !DisplayNameAvaliability;
            }
        }

    }
}
