using StructureMap;
using DI.Reminder.Data.DataBase;

namespace DI.Reminder.Data
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<IDataService>().Use<DataService>();
            For<IGetData>().Use<AppDatabase>();
        }
    }
}
