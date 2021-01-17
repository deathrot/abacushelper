using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Logic.Providers
{
    public class UserProvider
    {
        public async Task<bool> CheckEmailAlreadyRegistered(Logic.Interfaces.IConnectionUtility connectionUtility, string emailAddressToCheck)
        {
            var totalusers = await Logic.DB.DBUtility.GetScalar<int>(connectionUtility, @"Select COUNT(1) From students Where student_email = @email AND is_deleted = 0", 
                new Dictionary<string, object>{
                    {"email", emailAddressToCheck}
                });

            if ( totalusers > 0)
            {
                return true;
            }
            
            return false;
        }
        public async Task<bool> CheckDisplayNameAlreadyUsed(Logic.Interfaces.IConnectionUtility connectionUtility, string displayName)
        {
            var totalusers = await Logic.DB.DBUtility.GetScalar<int>(connectionUtility, "Select COUNT(1) From students Where student_display_name = @displayName AND is_deleted = 0", 
                new Dictionary<string, object>{
                    {"displayName", displayName}
            });

            if ( totalusers > 0)
            {
                return true;
            }
            
            return false;
        }


        public async Task<Logic.ViewModels.UserVM> GetUser(Interfaces.IConnectionUtility connectionUtility, string email_address)
        {
            using (var conn = connectionUtility.GetConnection())
            {
                return await getUser(conn, email_address);
            }
        }

        private async Task<Logic.ViewModels.UserVM> getUser(System.Data.IDbConnection connection, string email_address)
        {
            var entities = await DB.DBUtility.GetData<DBModels.UserEntity>(connection, "select * from users where is_deleted = 0 AND user_email = @email",
                                new System.Collections.Generic.Dictionary<string, object>() { { "email", email_address } });

            if (entities != null && entities.Count() == 1)
            {
                return Mappers.ObjectMapper.Instance.Mapper.Map<Logic.ViewModels.UserVM>(entities.ElementAt(0));
            }

            return null;
        }

        public async Task<Logic.Models.CreateUserResult> CreateUser(Interfaces.IConnectionUtility connectionUtility, DBModels.UserEntity userEntity, string password)
        {
            var result = new Logic.Models.CreateUserResult();
            using (var conn = connectionUtility.GetConnection())
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
                    parameters.Add("token", Constants.Constants.TOKEN_TEXT);

                    string createdUserId = await DB.DBUtility.GetScalar<string>(conn, @"
                    SET @UUID = UUID();
                    
                    INSERT INTO USERS(id, user_email, login_password, last_login_on, last_log_out, is_locked_out, is_confirmed, 
                    num_of_failed_password_attempt, is_deleted, modified_on)
                    VALUES(@UUID, @user_email, AES_ENCRYPT(@login_password, @token), NULL, NULL, FALSE, TRUE, 0, FALSE, UTC_TIMESTAMP());

                    SELECT @UUID;
                    ", parameters);

                    Guid uuid;
                    if (createdUserId != null && Guid.TryParse(createdUserId, out uuid))
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
                {"token", Constants.Constants.TOKEN_TEXT},
            };

            string authenticateSQL = @"
                
                Select id, is_deleted, is_locked_out, IF(cast(aes_decrypt(login_password, @token) as char(50)) = @password, TRUE, FALSE) as 'password_check_passed'
                From users where user_email = @email;

            ";

            var result = await DB.DBUtility.GetData<Models.AuthenticateResult>(connection, authenticateSQL, parameters);

            if (result != null && result.Count() == 1)
            {
                return result.ElementAt(0);
            }

            return null;
        }
        
        async Task increasePasswordFailCount(System.Data.IDbConnection conn, string user_id)
        {
            System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>()
            {
                {"user_id", user_id }
            };

            string increasePasswordFailCountSql = @"
                Update users 
                SET num_of_failed_password_attempt = num_of_failed_password_attempt + 1
                Where id = @user_id;
            ";

            await DB.DBUtility.GetScalar<int>(conn, increasePasswordFailCountSql, parameters);
        }

        async Task<DBModels.SessionsEntity> createSession(System.Data.IDbConnection conn, string user_id, 
                                                            string session_token, int sessionTimeoutInSeconds)
        {
            DateTime utc = DateTime.UtcNow;
            var session = new DBModels.SessionsEntity
            {
                id = Guid.NewGuid().ToString(),
                session_token = session_token,
                last_activity_time = utc,
                login_method = Enums.LoginMethod.Web,
                login_time = utc,
                next_login_timeout = utc.AddSeconds(sessionTimeoutInSeconds),
                user_id = user_id
            };
            
            object insertedSessionId = await DB.DBUtility.Insert<DBModels.SessionsEntity>(conn, new[] { session });

            if (insertedSessionId != null && !string.IsNullOrEmpty(insertedSessionId.ToString()))
            {
                return session;
            }

            return null;
        }


        public async Task<ViewModels.AuthenticateResultVM> Login(Interfaces.IConnectionUtility connectionUtility, string email, string password, 
                                                                    Interfaces.ISessionTokenProvider provider,
                                                                     int? sessionTimeoutInSeconds = null)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            sessionTimeoutInSeconds = sessionTimeoutInSeconds.HasValue ? sessionTimeoutInSeconds.Value : Constants.Constants.SESSION_TIMEOUT_IN_SECONDS;

            using (var conn = connectionUtility.GetConnection())
            {
                try
                {
                    var entity = await authenticate(conn, email, password);
                    DBModels.SessionsEntity sessionEntity = null;

                    if ( entity.ResultType == Enums.AuthenticateResultType.Success)
                    {
                        sessionEntity = await createSession(conn, entity.id, provider.GetToken(), sessionTimeoutInSeconds.Value);
                    }
                    else if ( entity.ResultType == Enums.AuthenticateResultType.PasswordDoesNotMatch)
                    {
                        await increasePasswordFailCount(conn, entity.id);
                    }

                    ViewModels.AuthenticateResultVM result = new ViewModels.AuthenticateResultVM()
                    {
                        ResultType = entity.ResultType,
                        Session = Mappers.ObjectMapper.Instance.Mapper.Map<ViewModels.SessionVM>(sessionEntity)
                    };

                    return result;
                }
                finally
                {

                }
            }
        }

        public async Task<bool> Logout(Interfaces.IConnectionUtility connectionUtility, string sessionToken)
        {
            if (string.IsNullOrEmpty(sessionToken))
                return false;

            using (var conn = connectionUtility.GetConnection())
            {
                try
                {
                    string logoutSql = @"
                        SET @user_id = '';

                        Select @user_id = user_id From 
                        sessions where session_token = @session_token;
                        
                        Update users
                        SET last_log_out = UTC_TIMESTAMP()
                        Where id = @user_id;

                        INSERT INTO session_archives(id, user_id, session_token, login_time, last_activity_time, login_method, log_out_time, is_deleted, modified_on)
                        Select id, user_id, session_token, login_time, last_activity_time, login_method, UTC_TIMESTAMP(), 0, UTC_TIMESTAMP()
                        from sessions
                        Where session_token = @session_token;

                        delete from sessions
                        Where session_token = @session_token;

                        Select ROW_COUNT();
                    ";

                    var parameters = new System.Collections.Generic.Dictionary<string, object>();
                    parameters.Add("@session_token", sessionToken);

                    int result = await DB.DBUtility.GetScalar<int>(conn, logoutSql, parameters);

                    if (result > 0)
                    {
                        return true;
                    }
                }
                finally
                {

                }
            }

            return false;
        }

        public async Task<bool> ChangePassword(Interfaces.IConnectionUtility connectionUtility, string userId, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(userId))
                return false;

            using (var conn = connectionUtility.GetConnection())
            {
                try
                {
                    string logoutSql = @"
                        Update users
                        SET login_password = aes_encrypt(@password, @token),
                        modified_on = UTC_TIMESTAMP()
                        Where id = @user_id AND cast(aes_decrypt(login_password, @token) AS CHAR(50)) = @oldPassword;

                        Select ROW_COUNT();
                    ";

                    var parameters = new System.Collections.Generic.Dictionary<string, object>();
                    parameters.Add("@user_id", userId);
                    parameters.Add("@oldPassword", oldPassword);
                    parameters.Add("@password", newPassword);
                    parameters.Add("@token", Constants.Constants.TOKEN_TEXT);

                    int result = await DB.DBUtility.GetScalar<int>(conn, logoutSql, parameters);

                    if (result > 0)
                    {
                        return true;
                    }

                }
                finally
                {

                }
            }

            return false;
        }

        public async Task<bool> ChangePassword(Interfaces.IConnectionUtility connectionUtility, ViewModels.SessionVM session, string newPassword)
        {
            if (session == null)
                return false;

            using (var conn = connectionUtility.GetConnection())
            {
                try
                {
                    var sessionValidResult = await IsSessionValid(conn, session.SessionToken, session.UserId);
                    if (sessionValidResult.IsValid)
                    {
                        string logoutSql = @"
                        Update users
                        SET login_password = aes_encrypt(@password, @token),
                        modified_on = UTC_TIMESTAMP()
                        Where id = @user_id;

                        Select ROW_COUNT();
                    ";

                        var parameters = new System.Collections.Generic.Dictionary<string, object>();
                        parameters.Add("@user_id", session.UserId);
                        parameters.Add("@sessionid", session.Id);

                        int result = await DB.DBUtility.GetScalar<int>(conn, logoutSql, parameters);

                        if (result > 0)
                        {
                            return true;
                        }
                    }
                }
                finally
                {

                }
            }

            return false;
        }

        public async Task<Models.SessionValidResult> IsSessionValid(Interfaces.IConnectionUtility connectionUtility, string sessionToken, string user_id)
        {
            using(var conn = connectionUtility.GetConnection())
            {
                return await IsSessionValid(conn, sessionToken, user_id);
            }
        }

        public async Task<Models.SessionValidResult> IsSessionValid(System.Data.IDbConnection conn, string sessionToken, string user_id)
        {
            Models.SessionValidResult result = new Models.SessionValidResult();

            if (string.IsNullOrEmpty(sessionToken) || string.IsNullOrEmpty(user_id))
                return result;

            try
            {
                string sessionValidSql = @"
                        Select next_login_timeout 
                        from sessions 
                        where session_token = @sessionToken AND user_id = @userId;
                    ";

                var parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("@sessionToken", sessionToken);
                parameters.Add("@userId", user_id);

                DateTime? next_login_timeout = await DB.DBUtility.GetScalar<DateTime?>(conn, sessionValidSql, parameters);

                result.NextLoginTimeout = next_login_timeout;
                if (next_login_timeout.HasValue && DateTime.UtcNow < next_login_timeout.Value)
                {
                    result.IsValid = true;
                }
            }
            finally
            {

            }

            return result;
        }

        public async Task<bool> RenewSession(Interfaces.IConnectionUtility connectionUtility, ViewModels.SessionVM session, int? timeoutInSeconds = null)
        {
            if (session == null)
                return false;

            timeoutInSeconds = timeoutInSeconds ?? Constants.Constants.SESSION_TIMEOUT_IN_SECONDS;

            using (var conn = connectionUtility.GetConnection())
            {
                try
                {
                    var sessionResult = await IsSessionValid(conn, session.SessionToken, session.UserId);
                    if (!sessionResult.IsValid)
                    {
                        return false;
                    }

                    string logoutSql = @"
                        Update sessions
                        SET next_login_timeout = @next_login_timeout,
                        modified_on = UTC_TIMESTAMP()
                        Where id = @user_id;

                        Select ROW_COUNT();
                    ";

                    var parameters = new System.Collections.Generic.Dictionary<string, object>();
                    parameters.Add("@user_id", session.UserId);
                    parameters.Add("@sessionid", session.Id);
                    parameters.Add("@next_login_timeout", sessionResult.NextLoginTimeout.Value.AddSeconds(timeoutInSeconds.Value));

                    int result = await DB.DBUtility.GetScalar<int>(conn, logoutSql, parameters);

                    if (result == 1)
                    {
                        return true;
                    }
                }
                finally
                {

                }
            }

            return false;
        }

    }
}