
namespace Logic.Mappers
{
    public class ObjectMapper
    {

        static ObjectMapper _Instance = new ObjectMapper();
        public static ObjectMapper Instance
        {
            get
            {
                return _Instance;
            }
        }

        public AutoMapper.IMapper Mapper { get; private set; }

        private ObjectMapper()
        {
            var config = buildConfiguration();

            Mapper = config.CreateMapper();
        }

        AutoMapper.MapperConfiguration buildConfiguration()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DBModels.QuestionEntity, ViewModels.QuestionVM>()
                .ForMember(x => x.Id, y => y.MapFrom(t => t.id))
                .ForMember(x => x.Description, y => y.MapFrom(t => t.record_description))
                .ForMember(x => x.Level, y => y.MapFrom(t => t.level))
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(t => t.modified_on))
                .ForMember(x => x.Name, y => y.MapFrom(t => t.record_name))
                .ForMember(x => x.QuestionJSON, y => y.MapFrom(t => t.question))
                .ForMember(x => x.SortOrder, y => y.MapFrom(t => t.sort_order))
                .ForMember(x => x.SubLevel, y => y.MapFrom(t => t.sub_level))
                .AfterMap((x, y) =>
                {
                    y.QuestionType = x.question_type.ToString();
                    y.QuestionSubType = x.question_sub_type.ToString();
                    y.EntityState = Enums.EntityState.None;
                    y.Severity = x.severity.ToString();
                });

                cfg.CreateMap<ViewModels.QuestionVM, DBModels.QuestionEntity>()
                .ForMember(x => x.id, y => y.MapFrom(t => t.Id))
                .ForMember(x => x.record_description, y => y.MapFrom(t => t.Description))
                .ForMember(x => x.level, y => y.MapFrom(t => t.Level))
                .ForMember(x => x.modified_on, y => y.MapFrom(t => t.ModifiedOn))
                .ForMember(x => x.record_name, y => y.MapFrom(t => t.Name))
                .ForMember(x => x.question, y => y.MapFrom(t => t.QuestionJSON))
                .ForMember(x => x.sort_order, y => y.MapFrom(t => t.SortOrder))
                .ForMember(x => x.sub_level, y => y.MapFrom(t => t.SubLevel))
                .AfterMap((x, y) =>
                {
                    y.question_type = (Enums.QuestionType)System.Enum.Parse<Enums.QuestionType>(x.QuestionType, true);
                    y.question_sub_type = (Enums.QuestionSubType)System.Enum.Parse<Enums.QuestionSubType>(x.QuestionSubType, true);
                    y.severity = (Enums.Severity)System.Enum.Parse<Enums.Severity>(x.Severity, true);
                });

                cfg.CreateMap<DBModels.SettingEntity, ViewModels.SettingVM>()
                .ForMember(x => x.Id, y => y.MapFrom(t => t.id))
                .ForMember(x => x.DataType, y => y.MapFrom(t => t.setting_data_type))
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(t => t.modified_on))
                .ForMember(x => x.Name, y => y.MapFrom(t => t.setting_name))
                .ForMember(x => x.Value, y => y.MapFrom(t => t.setting_value))
                .ForMember(x => x.SortOrder, y => y.MapFrom(t => t.sort_order))
                .AfterMap((x, y) =>
                {
                    y.EntityState = Enums.EntityState.None;
                });

                cfg.CreateMap<ViewModels.SettingVM, DBModels.SettingEntity>()
                .ForMember(x => x.id, y => y.MapFrom(t => t.Id))
                .ForMember(x => x.setting_name, y => y.MapFrom(t => t.Name))
                .ForMember(x => x.setting_value, y => y.MapFrom(t => t.Value))
                .ForMember(x => x.modified_on, y => y.MapFrom(t => t.ModifiedOn))
                .ForMember(x => x.sort_order, y => y.MapFrom(t => t.SortOrder))
                .ForMember(x => x.setting_data_type, y => y.MapFrom(t => t.DataType));

                cfg.CreateMap<DBModels.TagsEntity, ViewModels.TagVM>()
                .ForMember(x => x.Id, y => y.MapFrom(t => t.id))
                .ForMember(x => x.TagName, y => y.MapFrom(t => t.tag_name))
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(t => t.modified_on))
                .AfterMap((x, y) =>
                {
                    y.EntityState = Enums.EntityState.None;
                });

                cfg.CreateMap<ViewModels.TagVM, DBModels.TagsEntity>()
                .ForMember(x => x.id, y => y.MapFrom(t => t.Id))
                .ForMember(x => x.tag_name, y => y.MapFrom(t => t.TagName))
                .ForMember(x => x.modified_on, y => y.MapFrom(t => t.ModifiedOn));


                cfg.CreateMap<DBModels.TagsEntity, ViewModels.TagVM>()
                .ForMember(x => x.Id, y => y.MapFrom(t => t.id))
                .ForMember(x => x.TagName, y => y.MapFrom(t => t.tag_name))
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(t => t.modified_on))
                .AfterMap((x, y) =>
                {
                    y.EntityState = Enums.EntityState.None;
                });

                cfg.CreateMap<ViewModels.TagVM, DBModels.TagsEntity>()
                .ForMember(x => x.id, y => y.MapFrom(t => t.Id))
                .ForMember(x => x.tag_name, y => y.MapFrom(t => t.TagName))
                .ForMember(x => x.modified_on, y => y.MapFrom(t => t.ModifiedOn));

                cfg.CreateMap<ViewModels.UserVM, DBModels.UserEntity>()
                .ForMember(x => x.id, y => y.MapFrom(t => t.Id))
                .ForMember(x => x.is_confirmed, y => y.MapFrom(t => t.IsConfirmed))
                .ForMember(x => x.is_deleted, y => y.MapFrom(t => t.IsDeleted))
                .ForMember(x => x.is_locked_out, y => y.MapFrom(t => t.IsLockedOut))
                .ForMember(x => x.last_login_on, y => y.MapFrom(t => t.LastLoginOn))
                .ForMember(x => x.last_log_out, y => y.MapFrom(t => t.LastLogOut))
                .ForMember(x => x.modified_on, y => y.MapFrom(t => t.ModifiedOn))
                .ForMember(x => x.num_of_failed_password_attempt, y => y.MapFrom(t => t.NumberOfFailedPasswordAttempt))
                .ForMember(x => x.user_email, y => y.MapFrom(t => t.UserEmail));

                cfg.CreateMap<DBModels.UserEntity, ViewModels.UserVM>()
                .ForMember(x => x.Id, y => y.MapFrom(t => t.id))
                .ForMember(x => x.IsConfirmed, y => y.MapFrom(t => t.is_confirmed))
                .ForMember(x => x.IsDeleted, y => y.MapFrom(t => t.is_deleted))
                .ForMember(x => x.IsLockedOut, y => y.MapFrom(t => t.is_locked_out))
                .ForMember(x => x.LastLoginOn, y => y.MapFrom(t => t.last_login_on))
                .ForMember(x => x.LastLogOut, y => y.MapFrom(t => t.last_log_out))
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(t => t.modified_on))
                .ForMember(x => x.NumberOfFailedPasswordAttempt, y => y.MapFrom(t => t.num_of_failed_password_attempt))
                .ForMember(x => x.UserEmail, y => y.MapFrom(t => t.user_email));


                cfg.CreateMap<ViewModels.SessionVM, DBModels.SessionsEntity>()
                .ForMember(x => x.id, y => y.MapFrom(t => t.Id))
                .ForMember(x => x.session_token, y => y.MapFrom(t => t.SessionToken))
                .ForMember(x => x.last_activity_time, y => y.MapFrom(t => t.LastActivityTime))
                .ForMember(x => x.is_deleted, y => y.MapFrom(t => t.IsDeleted))
                .ForMember(x => x.login_time, y => y.MapFrom(t => t.LoginTime))
                .ForMember(x => x.modified_on, y => y.MapFrom(t => t.ModifiedOn))
                .ForMember(x => x.next_login_timeout, y => y.MapFrom(t => t.NextLoginTimeout))
                .ForMember(x => x.user_id, y => y.MapFrom(t => t.UserId));


                cfg.CreateMap<DBModels.SessionsEntity, ViewModels.SessionVM>()
                .ForMember(x => x.Id, y => y.MapFrom(t => t.id))
                .ForMember(x => x.SessionToken, y => y.MapFrom(t => t.session_token))
                .ForMember(x => x.LastActivityTime, y => y.MapFrom(t => t.last_activity_time))
                .ForMember(x => x.IsDeleted, y => y.MapFrom(t => t.is_deleted))
                .ForMember(x => x.LoginTime, y => y.MapFrom(t => t.login_time))
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(t => t.modified_on))
                .ForMember(x => x.NextLoginTimeout, y => y.MapFrom(t => t.next_login_timeout))
                .ForMember(x => x.UserId, y => y.MapFrom(t => t.user_id));

                
                cfg.CreateMap<DBModels.StudentEntity, ViewModels.StudentVM>()
                .ForMember(x => x.Id, y => y.MapFrom(t => t.id))
                .ForMember(x => x.IsDeleted, y => y.MapFrom(t => t.is_deleted))
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(t => t.modified_on))
                .ForMember(x => x.IsLockedOut, y => y.MapFrom(t => t.is_locked_out))
                .ForMember(x => x.StudentName, y => y.MapFrom(t => t.student_name))
                .ForMember(x => x.StudentDisplayName, y => y.MapFrom(t => t.student_display_name))
                .ForMember(x => x.StartingLevelId, y => y.MapFrom(t => t.starting_level_id))
                .ForMember(x => x.StartingSubLevelId, y => y.MapFrom(t => t.starting_sub_level_id))
                .ForMember(x => x.CurrentLevelId, y => y.MapFrom(t => t.current_level_id))
                .ForMember(x => x.CurrentSubLevelId, y => y.MapFrom(t => t.current_sub_level_id))
                .ForMember(x => x.LastLoginOn, y => y.MapFrom(t => t.last_login_on))
                .ForMember(x => x.LastLogOut, y => y.MapFrom(t => t.last_log_out));

                
                cfg.CreateMap<ViewModels.StudentVM,DBModels.StudentEntity>()
                .ForMember(x => x.id, y => y.MapFrom(t => t.Id))
                .ForMember(x => x.is_deleted, y => y.MapFrom(t => t.IsDeleted))
                .ForMember(x => x.modified_on, y => y.MapFrom(t => t.ModifiedOn))
                .ForMember(x => x.is_locked_out, y => y.MapFrom(t => t.IsLockedOut))
                .ForMember(x => x.student_name, y => y.MapFrom(t => t.StudentName))
                .ForMember(x => x.student_display_name, y => y.MapFrom(t => t.StudentDisplayName))
                .ForMember(x => x.starting_level_id, y => y.MapFrom(t => t.StartingLevelId))
                .ForMember(x => x.starting_sub_level_id, y => y.MapFrom(t => t.StartingSubLevelId))
                .ForMember(x => x.current_level_id, y => y.MapFrom(t => t.CurrentLevelId))
                .ForMember(x => x.current_sub_level_id, y => y.MapFrom(t => t.CurrentSubLevelId))
                .ForMember(x => x.last_login_on, y => y.MapFrom(t => t.LastLoginOn))
                .ForMember(x => x.last_log_out, y => y.MapFrom(t => t.LastLogOut));
            });

            return config;
        }
    }
}