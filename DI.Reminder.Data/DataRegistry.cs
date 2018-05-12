using StructureMap;

namespace DI.Reminder.Data
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<IDataService>().Use<DataService>();
        }
    }
}
