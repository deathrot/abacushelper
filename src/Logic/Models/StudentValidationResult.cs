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

        public Enums.AvailiabilityEnum EmailAvailiability {get; set;} = Enums.AvailiabilityEnum.None;

        public Enums.AvailiabilityEnum DisplayNameAvaliability {get; set;} = Enums.AvailiabilityEnum.None;

        public bool HasError {
            get
            {
                return EmailError || DisplayNameError || PasswordError || NameError || EmailAvailiability != Enums.AvailiabilityEnum.Available 
                    || DisplayNameAvaliability != Enums.AvailiabilityEnum.Available;
            }
        }

    }
}
