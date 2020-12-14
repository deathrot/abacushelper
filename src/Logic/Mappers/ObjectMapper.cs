
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
                cfg.CreateMap<DBModels.AbacusLevels, ViewModels.AbacusLevelVM>()
                .ForMember(x => x.id, y => y.MapFrom(t => t.Id))
                .ForMember(x => x.name, y => y.MapFrom(t => t.RecordName))
                .ForMember(x => x.desc, y => y.MapFrom(t => t.RecordDescription))
                .ForMember(x => x.sort, y => y.MapFrom(t => t.SortOrder))
                .ForMember(x => x.modified_on, y => y.MapFrom(t => t.ModifiedOn));

                cfg.CreateMap<ViewModels.AbacusLevelVM, DBModels.AbacusLevels>()
                .ForMember(x => x.Id, y => y.MapFrom(t => t.id))
                .ForMember(x => x.RecordName, y => y.MapFrom(t => t.name))
                .ForMember(x => x.RecordDescription, y => y.MapFrom(t => t.desc))
                .ForMember(x => x.SortOrder, y => y.MapFrom(t => t.sort));

            });

            return config;
        }
    }
}