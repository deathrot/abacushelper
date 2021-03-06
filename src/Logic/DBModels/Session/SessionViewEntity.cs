using System;

namespace Logic.DBModels
{
    [Dapper.Contrib.Extensions.Table("sessions_view")]
    public class SessionViewEntity : DBEntity, Interfaces.IDBEntity
    {

        public string user_id { get; set; }

        public DateTime login_time { get; set; }

        public DateTime last_activity_time { get; set; }

        public DateTime next_login_timeout { get; set; }

        public Enums.LoginMethod login_method { get; set; }

        public string additional_session_data { get; set; }

        public string session_token { get; set; }

        public string student_display_name { get; set; }

        public string student_email { get; set; }

    }
}