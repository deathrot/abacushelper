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

        private ObjectMapper()
        {
            //AutoMapper.Configuration.
        }
    }
}