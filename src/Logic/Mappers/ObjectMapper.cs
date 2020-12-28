
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

            });

            return config;
        }
    }
}