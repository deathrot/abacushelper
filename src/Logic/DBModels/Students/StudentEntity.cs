using System;

namespace Logic.DBModels
{

    [Dapper.Contrib.Extensions.Table("students")]
    public class StudentEntity : DBEntity, Interfaces.IDBEntity
    {

        public string student_name { get; set; }
        public string student_display_name { get; set; }
        public string student_email{get; set;}
        public decimal? starting_level_id {get; set;}
        public decimal? starting_sub_level_id {get; set;}
        public decimal? current_level_id {get; set;}
        public decimal? current_sub_level_id {get; set;}
        public DateTime? last_login_on{get; set;}
        public DateTime? last_log_out{get; set;}
        public bool is_locked_out{get; set;}

    }
}