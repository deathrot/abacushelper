using System;
using System.Threading.Tasks;
using System.Linq;

namespace Logic.Providers
{
    public class UserProvider
    {

        public async Task<Logic.ViewModels.UserVM> Get(Interfaces.IConnectionUtility connectionUtility, string email_address)
        {
            using ( var conn = connectionUtility.GetConnection())
            {
                return await Get(conn, email_address);
            }
        }

        public async Task<Logic.ViewModels.UserVM> Get(System.Data.IDbConnection connection, string email_address)
        {
            var entities = await DB.DBUtility.GetData<DBModels.UserEntity>(connection, "select * from users where is_deleted = 0 AND user_email = @email", 
                                new System.Collections.Generic.Dictionary<string, object>(){{"email", email_address}});

            if ( entities != null && entities.Count() == 1)
            {
                return Mappers.ObjectMapper.Instance.Mapper.Map<Logic.ViewModels.UserVM>(entities.ElementAt(0));
            }

            return null;
        }

        public async Task<Logic.Models.CreateUserResult> CreateUser(Interfaces.IConnectionUtility connectionUtility, DBModels.UserEntity userEntity, string password)
        {
            var result = new Logic.Models.CreateUserResult();
            using(var conn = connectionUtility.GetConnection())
            {
                try
                {
                    result.Success = true;

                    var entity = await Get(connectionUtility, userEntity.user_email);

                    if (entity != null)
                    {
                        result.Success = false;
                        result.Reason = Enums.CreateUserReason.UserAlreadyExist;
                        return result;
                    }

                    var parameters = new System.Collections.Generic.Dictionary<string, object>();
                    parameters.Add("user_email", userEntity.user_email);
                    parameters.Add("login_password", password);
                    parameters.Add("token", Constants.Constants.TOKEN_TEXT);

                    string createdUserId = await DB.DBUtility.GetScalar<string>(conn, @"
                    SET @UUID = UUID();
                    
                    INSERT INTO USERS(id, user_email, login_password, last_login_on, last_log_out, is_locked_out, is_confirmed, 
                    num_of_failed_password_attempt, is_deleted, modified_on)
                    VALUES(@UUID, @email, AES_ENCRYPT(@PASSWORD, @token), NULL, NULL, FALSE, TRUE, 0, FALSE, UTC_TIMESTAMP());

                    SELECT @UUID;
                    ", parameters);

                    Guid uuid;
                    if ( createdUserId != null && Guid.TryParse(createdUserId, out uuid))
                    {
                        result.UserId = createdUserId.ToString();
                        result.Success = true;
                    }
                }
                finally
                {

                }
            }

            return result;
        }

        public async Task<ViewModels.SessionVM> Login(Interfaces.IConnectionUtility connectionUtility, string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            using (var conn = connectionUtility.GetConnection())
            {
                try
                {
                    var entity = await login(conn, email, password);

                    if (entity != null)
                    {
                    var parameters = new System.Collections.Generic.Dictionary<string, object>();
                    parameters.Add("user_email", userEntity.user_email);
                    parameters.Add("login_password", password);
                    parameters.Add("token", Constants.Constants.TOKEN_TEXT);

                    object createdUserId = DB.DBUtility.ExecuteScalar(connectionUtility, @"
                SET @UUID = UUID();
                
                INSERT INTO USERS(id, user_email, login_password, last_login_on, last_log_out, is_locked_out, is_confirmed, 
                num_of_failed_password_attempt, is_deleted, modified_on)
                VALUES(@UUID, @email, AES_ENCRYPT(@PASSWORD, @token), NULL, NULL, FALSE, TRUE, 0, FALSE, UTC_TIMESTAMP());

                SELECT @UUID;
                ", parameters);

                    Guid uuid;
                    if (createdUserId != null && Guid.TryParse(createdUserId.ToString(), out uuid))
                    {
                        result.UserId = createdUserId.ToString();
                        result.Success = true;
                    }
                    }
                }
                finally
                {

                }
            }

            return result;
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