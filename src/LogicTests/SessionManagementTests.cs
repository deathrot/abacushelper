using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LogicTests
{
    public class Tests
    {
        Logic.Interfaces.IConnectionUtility studentConnectionUtility = new Logic.DB.StudentDBConnectionUtility("Server=127.0.0.1;Database=students;Uid=abacus;Pwd=Abacus2020@;Allow User Variables=True");
        Logic.Interfaces.IConnectionUtility baseDataConnectionUtility = new Logic.DB.BaseDBConnectionUtility("Server=127.0.0.1;Database=base_data;Uid=abacus;Pwd=Abacus2020@;Allow User Variables=True");

        [SetUp]
        public void Setup()
        {
        }

        async Task undoChanges()
        {
            await Logic.DB.DBUtility.GetScalar<int>(studentConnectionUtility, @"Delete From users;Delete from sessions;Delete from session_archives;", null);
        }

        [Test]
        public async Task CreateUserTests_UserCreatedSuccessfully()
        {
            Logic.Providers.UserProvider provider = new Logic.Providers.UserProvider();

            await undoChanges();

            var userEntity = new Logic.DBModels.UserEntity()
            {
                id = Guid.NewGuid().ToString(),
                is_deleted = false,
                is_confirmed = true,
                is_locked_out = false,
                user_email = "test@test.com",
                modified_on = DateTime.UtcNow,
                num_of_failed_password_attempt = 0
            };
            
            var result = await provider.CreateUser(studentConnectionUtility, userEntity, "testpassword");

            Assert.IsTrue(result.Success);
            Guid guid;
            Assert.IsTrue(Guid.TryParse(result.UserId, out guid));

            Assert.Pass();
        }

        [Test]
        public async Task CreateUserTests_AttempLogin_LoginSuccessfull()
        {
            Logic.Providers.UserProvider provider = new Logic.Providers.UserProvider();

            await undoChanges();

            var userEntity = new Logic.DBModels.UserEntity()
            {
                id = Guid.NewGuid().ToString(),
                is_deleted = false,
                is_confirmed = true,
                is_locked_out = false,
                user_email = "test@test.com",
                modified_on = DateTime.UtcNow,
                num_of_failed_password_attempt = 0
            };

            var result = await provider.CreateUser(studentConnectionUtility, userEntity, "testpassword");

            Assert.IsTrue(result.Success);
            Guid guid;
            Assert.IsTrue(Guid.TryParse(result.UserId, out guid));

            var testTokenProvider = new TestLoginTokenProvider();

            var loginResult = await provider.Login(studentConnectionUtility, "test@test.com", "testpassword", testTokenProvider);

            Assert.IsTrue(loginResult.ResultType == Logic.Enums.AuthenticateResultType.Success);
            Assert.IsTrue(!string.IsNullOrEmpty(loginResult.Session.SessionToken));

            var sessionValid = await provider.IsSessionValid(studentConnectionUtility, loginResult.Session.SessionToken, loginResult.Session.UserId);
            Assert.IsTrue(sessionValid.IsValid);

            await provider.Logout(studentConnectionUtility, loginResult.Session.SessionToken);
            
            Assert.Pass();
        }

        [Test]
        public async Task CreateUserTests_AttempLoginAndWaitForAutomaticLogOut_LogOutHappensSuccessfully()
        {
            Logic.Providers.UserProvider provider = new Logic.Providers.UserProvider();

            await undoChanges();

            var userEntity = new Logic.DBModels.UserEntity()
            {
                id = Guid.NewGuid().ToString(),
                is_deleted = false,
                is_confirmed = true,
                is_locked_out = false,
                user_email = "test@test.com",
                modified_on = DateTime.UtcNow,
                num_of_failed_password_attempt = 0
            };

            var result = await provider.CreateUser(studentConnectionUtility, userEntity, "testpassword");

            Assert.IsTrue(result.Success);
            Guid guid;
            Assert.IsTrue(Guid.TryParse(result.UserId, out guid));

            var testTokenProvider = new TestLoginTokenProvider();

            var loginResult = await provider.Login(studentConnectionUtility, "test@test.com", "testpassword", testTokenProvider, 15);

            Assert.IsTrue(loginResult.ResultType == Logic.Enums.AuthenticateResultType.Success);
            Assert.IsTrue(!string.IsNullOrEmpty(loginResult.Session.SessionToken));

            var sessionValid = await provider.IsSessionValid(studentConnectionUtility, loginResult.Session.SessionToken, loginResult.Session.UserId);
            Assert.IsTrue(sessionValid.IsValid);

            await Task.Delay(16000);

            sessionValid = await provider.IsSessionValid(studentConnectionUtility, loginResult.Session.SessionToken, loginResult.Session.UserId);
            Assert.IsFalse(sessionValid.IsValid);

            Assert.Pass();
        }


        [Test]
        public async Task CreateUserTests_AttempLoginAndRenewSession_RenewIsSuccessfull()
        {
            Logic.Providers.UserProvider provider = new Logic.Providers.UserProvider();

            await undoChanges();

            var userEntity = new Logic.DBModels.UserEntity()
            {
                id = Guid.NewGuid().ToString(),
                is_deleted = false,
                is_confirmed = true,
                is_locked_out = false,
                user_email = "test@test.com",
                modified_on = DateTime.UtcNow,
                num_of_failed_password_attempt = 0
            };

            var result = await provider.CreateUser(studentConnectionUtility, userEntity, "testpassword");

            Assert.IsTrue(result.Success);
            Guid guid;
            Assert.IsTrue(Guid.TryParse(result.UserId, out guid));

            var testTokenProvider = new TestLoginTokenProvider();

            var loginResult = await provider.Login(studentConnectionUtility, "test@test.com", "testpassword", testTokenProvider, 15);

            Assert.IsTrue(loginResult.ResultType == Logic.Enums.AuthenticateResultType.Success);
            Assert.IsTrue(!string.IsNullOrEmpty(loginResult.Session.SessionToken));

            var sessionValid = await provider.IsSessionValid(studentConnectionUtility, loginResult.Session.SessionToken, loginResult.Session.UserId);
            Assert.IsTrue(sessionValid.IsValid);

            await Task.Delay(5000);

            bool renewSession = await provider.RenewSession(studentConnectionUtility, loginResult.Session, 10);

            Assert.IsFalse(renewSession);

            Assert.Pass();
        }


        [Test]
        public async Task CreateUserTests_AttempLoginAndChangePasswordAfterLogout_ChangeIsNotSuccessfull()
        {
            Logic.Providers.UserProvider provider = new Logic.Providers.UserProvider();

            await undoChanges();

            var userEntity = new Logic.DBModels.UserEntity()
            {
                id = Guid.NewGuid().ToString(),
                is_deleted = false,
                is_confirmed = true,
                is_locked_out = false,
                user_email = "test@test.com",
                modified_on = DateTime.UtcNow,
                num_of_failed_password_attempt = 0
            };

            var result = await provider.CreateUser(studentConnectionUtility, userEntity, "testpassword");

            Assert.IsTrue(result.Success);
            Guid guid;
            Assert.IsTrue(Guid.TryParse(result.UserId, out guid));

            var testTokenProvider = new TestLoginTokenProvider();

            var loginResult = await provider.Login(studentConnectionUtility, "test@test.com", "testpassword", testTokenProvider, 8);

            Assert.IsTrue(loginResult.ResultType == Logic.Enums.AuthenticateResultType.Success);
            Assert.IsTrue(!string.IsNullOrEmpty(loginResult.Session.SessionToken));

            var sessionValid = await provider.IsSessionValid(studentConnectionUtility, loginResult.Session.SessionToken, loginResult.Session.UserId);
            Assert.IsTrue(sessionValid.IsValid);

            await Task.Delay(9000);

            var changePasswordResult = await provider.ChangePassword(studentConnectionUtility, loginResult.Session, "testpassword_2");

            Assert.IsFalse(changePasswordResult);

            Assert.Pass();
        }

        [Test]
        public async Task CreateUserTests_AttempLoginAndChangePasswordBeforeLogout_ChangeIsSuccessfull()
        {
            Logic.Providers.UserProvider provider = new Logic.Providers.UserProvider();

            await undoChanges();

            var userEntity = new Logic.DBModels.UserEntity()
            {
                id = Guid.NewGuid().ToString(),
                is_deleted = false,
                is_confirmed = true,
                is_locked_out = false,
                user_email = "test@test.com",
                modified_on = DateTime.UtcNow,
                num_of_failed_password_attempt = 0
            };

            var result = await provider.CreateUser(studentConnectionUtility, userEntity, "testpassword");

            Assert.IsTrue(result.Success);
            Guid guid;
            Assert.IsTrue(Guid.TryParse(result.UserId, out guid));

            var testTokenProvider = new TestLoginTokenProvider();

            var loginResult = await provider.Login(studentConnectionUtility, "test@test.com", "testpassword", testTokenProvider, 8);

            Assert.IsTrue(loginResult.ResultType == Logic.Enums.AuthenticateResultType.Success);
            Assert.IsTrue(!string.IsNullOrEmpty(loginResult.Session.SessionToken));

            var sessionValid = await provider.IsSessionValid(studentConnectionUtility, loginResult.Session.SessionToken, loginResult.Session.UserId);
            Assert.IsTrue(sessionValid.IsValid);

            await Task.Delay(5000);

            var changePasswordResult = await provider.ChangePassword(studentConnectionUtility, loginResult.Session, "testpassword_2");

            Assert.IsTrue(changePasswordResult);

            Assert.Pass();
        }

        [Test]
        public async Task CreateUserTests_AttemptToChangeThPasswordBySupplingCorrectOldPassword_ChangeIsSuccessfull()
        {
            Logic.Providers.UserProvider provider = new Logic.Providers.UserProvider();

            await undoChanges();

            var userEntity = new Logic.DBModels.UserEntity()
            {
                id = Guid.NewGuid().ToString(),
                is_deleted = false,
                is_confirmed = true,
                is_locked_out = false,
                user_email = "test@test.com",
                modified_on = DateTime.UtcNow,
                num_of_failed_password_attempt = 0
            };

            var result = await provider.CreateUser(studentConnectionUtility, userEntity, "testpassword");

            Assert.IsTrue(result.Success);
            Guid guid;
            Assert.IsTrue(Guid.TryParse(result.UserId, out guid));

            var changePasswordResult = await provider.ChangePassword(studentConnectionUtility, result.UserId, "testpassword", "testpassword_2");

            Assert.IsTrue(changePasswordResult);

            Assert.Pass();
        }
    }
}