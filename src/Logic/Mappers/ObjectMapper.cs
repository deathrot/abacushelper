
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
                .ForMember(x => x.LevelId, y => y.MapFrom(t => t.level_id))
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(t => t.modified_on))
                .ForMember(x => x.Name, y => y.MapFrom(t => t.record_name))
                .ForMember(x => x.QuestionJSON, y => y.MapFrom(t => t.question))
                .ForMember(x => x.Severity, y => y.MapFrom(t => t.severity))
                .ForMember(x => x.SortOrder, y => y.MapFrom(t => t.sort_order))
                .ForMember(x => x.SubLevelId, y => y.MapFrom(t => t.sub_level_id))
                .AfterMap((x, y) =>
                {
                    y.QuestionType = x.question_type.ToString();
                    y.EntityState = Enums.EntityState.None;
                });

                cfg.CreateMap<ViewModels.QuestionVM, DBModels.QuestionEntity>()
                .ForMember(x => x.id, y => y.MapFrom(t => t.Id))
                .ForMember(x => x.record_description, y => y.MapFrom(t => t.Description))
                .ForMember(x => x.level_id, y => y.MapFrom(t => t.LevelId))
                .ForMember(x => x.modified_on, y => y.MapFrom(t => t.ModifiedOn))
                .ForMember(x => x.record_name, y => y.MapFrom(t => t.Name))
                .ForMember(x => x.question, y => y.MapFrom(t => t.QuestionJSON))
                .ForMember(x => x.sort_order, y => y.MapFrom(t => t.SortOrder))
                .ForMember(x => x.sub_level_id, y => y.MapFrom(t => t.SubLevelId))
                .AfterMap((x, y) =>
                {
                    y.question_type = (Enums.QuestionType)System.Enum.Parse<Enums.QuestionType>(x.QuestionType);
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