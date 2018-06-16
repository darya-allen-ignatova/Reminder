using StructureMap;
using DI.Reminder.Data.PromptDataBase;
using DI.Reminder.Data.CategoryDataBase;
using DI.Reminder.Data.AccountDatabase;
using DI.Reminder.Data.RoleDatabase;
using DI.Reminder.Data.DService;
using DI.Reminder.Data.SearchingDatabase;

namespace DI.Reminder.Data.DataRegistry
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<IDataService>().Use<DataService>();
            For<IPromptRepository>().Use<PromptRepository>();
            For<ICategoryRepository>().Use<CategoryRepository>();
            For<ISearchService>().Use<SearchService>();
            For<IAccountRepository>().Use<AccountRepository>();
            For<IRoleRepository>().Use<RoleRepository>();
        }
    }
}
