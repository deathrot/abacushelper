using System;
using System.Threading.Tasks;


namespace Logic.Providers
{
    public class UserProvider
    {

        public async Task<Logic.ViewModels.QuestionVM[]> Get(Interfaces.IConnectionUtility connectionUtility, string email_address)
        {
            var entities = await DB.DBUtility.GetData<DBModels.UserEntity>(connectionUtility, "select * from questions where is_deleted = 0 AND user_email = @email", 
                                new System.Collections.Generic.Dictionary<string, object>(){{"email", email_address}});

            return Mappers.ObjectMapper.Instance.Mapper.Map<Logic.ViewModels.QuestionVM[]>(entities);
        }

        public async Task<Logic.Models.DBSaveResult> CreateUser(Interfaces.IConnectionUtility connectionUtility, DBModels.UserEntity userEntity, string password)
        {
            await Task.Delay(0);
            var result = new Logic.Models.DBSaveResult();
            try
            {
                result.Success = true;
            }
            finally
            {

            }

            return result;
        }

        public async Task<ViewModels.SessionVM> Login(Interfaces.IConnectionUtility connectionUtility, string email, string password) 
        {

        }
        
        public async Task<bool> Logout(Interfaces.IConnectionUtility connectionUtility, ViewModels.SessionVM session) 
        {
            
        }
        
        public async Task<bool> ChangePassword(Interfaces.IConnectionUtility connectionUtility, ViewModels.SessionVM session, string newPassword) 
        {
            
        }

        public async Task<bool> RenewSession(Interfaces.IConnectionUtility connectionUtility, ViewModels.SessionVM session) 
        {
            
        }
        
    }
}