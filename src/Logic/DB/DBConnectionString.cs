
namespace Logic.DB
{
    public class DBConnectionString
    {
        
        public string BaseDBConnectionString
        {
            get;
            private set;
        }

        public string StudentDBConnectionString
        {
            get;
            private set;
        }

        public DBConnectionString(string baseDBConnectionString, string studentDBConnectionString)
        {
            this.BaseDBConnectionString = baseDBConnectionString;
            this.StudentDBConnectionString = studentDBConnectionString;
        }

    }
}