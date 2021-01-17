using System.Threading.Tasks;

namespace Logic.Validators
{
    public class StudentValidator
    {

        public async Task<StudentValidationResult> ValidateStudent(Providers.StudentProvider provider, Interfaces.IConnectionUtility conn, ViewModels.StudentVM studentVM)
        {
            var result = new StudentValidationResult();

            result.DisplayNameError = !checkDisplayName(studentVM.StudentDisplayName);
            result.NameError = !checkName(studentVM.StudentName);
            result.EmailError = !checkEmail(studentVM.StudentEmail);
            
            if (!result.EmailError)
                result.EmailAvailiability = await checkEmailAvailability(provider, conn, studentVM.StudentEmail);
            
            if (!result.DisplayNameError)
                result.DisplayNameAvaliability = await checkDisplayNameAvailability(provider, conn, studentVM.StudentDisplayName);
            
            result.PasswordError = !checkPassword(studentVM.Password);

            return result;
        }

        async Task<Enums.AvailiabilityEnum> checkEmailAvailability(Providers.StudentProvider provider, Interfaces.IConnectionUtility connection, string email)
        {
            try
            {
                var result = await provider.CheckEmailAlreadyRegistered(connection, email);

                if (result)
                    return Enums.AvailiabilityEnum.NotAvailable;

                return Enums.AvailiabilityEnum.Available;
            }
            catch
            {
                return Enums.AvailiabilityEnum.Unknown;
            }
        }

        async Task<Enums.AvailiabilityEnum> checkDisplayNameAvailability(Providers.StudentProvider provider, Interfaces.IConnectionUtility connection, string displayName)
        {
            try
            {
                var result = await provider.CheckDisplayNameAlreadyUsed(connection, displayName);

                if (result)
                    return Enums.AvailiabilityEnum.NotAvailable;

                return Enums.AvailiabilityEnum.Available;
            }
            catch
            {
                return Enums.AvailiabilityEnum.Unknown;
            }
        }

        private bool checkEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool checkName(string name)
        {
            if (name != null && name.Trim().Length > 0 && name.Trim().Length <= 256)
            {
                return true;
            }

            return false;
        }

        private bool checkDisplayName(string name)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(name, "^[a-z0-9]{3,16}");
        }

        private bool checkPassword(string password)
        {
            if (password == null || string.IsNullOrEmpty(password) || password.Trim().Length == 0 || password.Trim().Length < 6)
            {
                return false;
            }

            return true;
        }
    }
}