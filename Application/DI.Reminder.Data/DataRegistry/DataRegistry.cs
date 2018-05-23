using StructureMap;
using DI.Reminder.Data.DataBase;
using DI.Reminder.Data.DService;

namespace DI.Reminder.Data.DataRegistry
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<IDataService>().Use<DataService>();
            For<IPromptRepository>().Use<PromptRepository>();
            For<ICategoryRepository>().Use<CategoryRepository>();
        }
    }
}
