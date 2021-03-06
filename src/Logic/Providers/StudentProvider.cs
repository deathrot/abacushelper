using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Logic.Providers
{
    public class StudentProvider : UserProvider
    {
        public async Task<Validators.StudentValidationResult> SaveStudent(Interfaces.IConnectionUtility connection, ViewModels.StudentVM student)
        {
            Validators.StudentValidator valid = new Validators.StudentValidator();
            var result = await valid.ValidateStudent(this, connection, student);

            if (!result.HasError)
            {
                bool dbResult = await saveStudent(connection, student);

                result.ValidationSuccess = dbResult;
            }

            return result;
        }

        private async Task<bool> saveStudent(Interfaces.IConnectionUtility connection, ViewModels.StudentVM student)
        {
            student.Id = Guid.NewGuid().ToString();

            using(var conn = connection.GetConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    await DB.DBUtility.Insert<DBModels.StudentEntity>(tran.Connection, 
                        new DBModels.StudentEntity[]{ Mappers.ObjectMapper.Instance.Mapper.Map<DBModels.StudentEntity>(student)});

                    var userEntity = new DBModels.UserEntity();
                    userEntity.id = student.Id;
                    userEntity.user_email = student.StudentEmail;
                    userEntity.is_confirmed = true;
                    userEntity.is_locked_out = false;
                    userEntity.num_of_failed_password_attempt = 0;

                    var createUserResult = await CreateUser(tran.Connection, userEntity, student.Password);

                    tran.Commit();

                    return createUserResult.Success;
                }
                catch
                {
                    tran.Rollback();
                }
                finally
                {
                    tran.Dispose();
                }
            }

            return false;
        }

    }
}