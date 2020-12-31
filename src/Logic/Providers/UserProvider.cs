using System;
using System.Threading.Tasks;
using System.Linq;

namespace Logic.Providers
{
    public class UserProvider
    {

        public async Task<Logic.ViewModels.UserVM> GetUser(Interfaces.IConnectionUtility connectionUtility, string email_address)
        {
            using ( var conn = connectionUtility.GetConnection())
            {
                return await getUser(conn, email_address);
            }
        }

        private async Task<Logic.ViewModels.UserVM> getUser(System.Data.IDbConnection connection, string email_address)
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

                    var entity = await getUser(conn, userEntity.user_email);

                    if (entity != null)
                    {
                        result.Success = false;
                        result.Reason = Enums.CreateUserReason.UserAlreadyExist;
                        return result;
                    }

                    var parameters = new System.Collections.Generic.Dictionary<string, object>();
                    parameters.Add("user_email", userEntity.user_email);
                    parameters.Add("login_password", password);
                    parameters.Add("token", Constants.Constants.ENCRYPTION_TOKEN);

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

        private async Task<Models.AuthenticateResult> authenticate(System.Data.IDbConnection connection, string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var parameters = new System.Collections.Generic.Dictionary<string, object>(){
                {"email", email},
                {"password", password},
                {"token", Constants.Constants.ENCRYPTION_TOKEN},
            };

            string authenticateSQL = @"
                SET @userId = '';
                SET @islocked = FALSE;
                SET @isdeleted = FALSE;
                SET @password_check_passed = FALSE;
                SET @sessionId = UUID();
                SET @time = UTC_TIMESTAMP();

                Select @userId = id, @islocked = is_locked_out, @isdeleted = is_deleted
                From users 
                where user_email = @email;
                                
                IF is_uuid(@userId) = TRUE
                THEN 
                    IF @isdeleted = TRUE
                    THEN
                        select TRUE 'is_deleted';
                    END IF

                    IF @islocked = TRUE
                    THEN
                        select TRUE 'is_locked';
                    END IF
                    
                    /*check password*/
                    IF @isdeleted = FALSE AND @islocked = FALSE
                    THEN 
                        Select @password_check_passed = NOT is_deleted 
                        From users where user_id = @userId AND 
                            cast(aes_decrypt(password, @token) as char(50)) = @password;

                    END IF

                    /*insert session here*/
                    IF @password_check_passed = TRUE AND 
                        @isdeleted = FALSE AND @islocked = FALSE
                    THEN 

                        INSERT INTO sessions(id, userid, login_time, last_activity_time, login_method,
	                        next_login_timeout, is_deleted, modified_on)
                        VALUES(@sessionId, @userid, @time, @time, @login_method, Date_Add(@time, INTERVAL 15 MINUTE));

                        Update users
                        SET last_login_on = @time,
                        last_log_out = COALESCE(last_log_out, @time),
                        modified_time = @time
                        Where id = @userid;
                        
                    END IF
                    
                END IF

                IF @password_check_passed = TRUE AND 
                        @isdeleted = FALSE AND @islocked = FALSE
                THEN 

                    Select @password_check_passed 'password_check_passed', @isdeleted 'user_is_deleted', 
                        @islocked 'user_is_locked', s.*
                    FROM sessions
                    where id = @sessionId; 
                
                ELSE 
                    Select @password_check_passed 'password_check_passed', @isdeleted 'user_is_deleted', 
                        @islocked 'user_is_locked';
                END IF

            ";

            var result = await DB.DBUtility.GetData<Models.AuthenticateResult>(connection, authenticateSQL, parameters);

            if ( result != null && result.Count() == 1)
            {
                return result.ElementAt(0);                
            }

            return null;
        }
        

        public async Task<ViewModels.AuthenticateResultVM> Login(Interfaces.IConnectionUtility connectionUtility, string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            using (var conn = connectionUtility.GetConnection())
            {
                try
                {
                    var entity = await authenticate(conn, email, password);
                    
                    return Mappers.ObjectMapper.Instance.Mapper.Map<ViewModels.AuthenticateResultVM>(entity);
                }
                finally
                {

                }
            }
        }
        
        public async Task<bool> Logout(Interfaces.IConnectionUtility connectionUtility, ViewModels.SessionVM session) 
        {
            if (session == null)
                return false;

            using (var conn = connectionUtility.GetConnection())
            {
                try
                {
                    string logoutSql = @"
                        Update users
                        SET last_log_out = UTC_TIMESTAMP()
                        Where id = @userid;

                        INSERT INTO sessions_archive(id, userid, login_time, login_method, log_out_time, is_deleted, modified_on)
                        Select id, userid, login_time, last_activity_time, login_method, UTC_TIMESTAMP(), 0, UTC_TIMESTAMP()
                        from session
                        Where id = @sessionid;

                        delete from sessions
                        Where id = @sessionid;

                        Select ROW_COUNT();
                    ";

                    var parameters = new System.Collections.Generic.Dictionary<string, object>();
                    parameters.Add("@userid", session.UserId);
                    parameters.Add("@sessionid", session.Id);
                    

                    int result = await DB.DBUtility.GetScalar<int>(conn, logoutSql, parameters);

                    if ( result > 0)
                    {
                        return true;
                    }
                }
                finally
                {

                }
            }

            returm false;
        }
        
        public async Task<bool> ChangePassword(Interfaces.IConnectionUtility connectionUtility, ViewModels.SessionVM session, string newPassword) 
        {
            
        }

        public async Task<bool> RenewSession(Interfaces.IConnectionUtility connectionUtility, ViewModels.SessionVM session) 
        {
            
        }
        
    }
}