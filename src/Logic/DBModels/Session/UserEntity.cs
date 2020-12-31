using System;

namespace Logic.DBModels
{

    [Dapper.Contrib.Extensions.Table("users")]
    public class UserEntity : DBEntity, Interfaces.IDBEntity
    {

        public string user_email { get; set; }
        public DateTime last_login_on { get; set; }
        public DateTime last_log_out { get; set; }
        public bool is_locked_out { get; set; }
        public bool is_confirmed { get; set; }
        public int num_of_failed_password_attempt { get; set; }

    }
}