using DI.Reminder.BL.Services;
using StructureMap;
using DI.Reminder.BL.CategoryStorage;
using DI.Reminder.BL.PromptStorage;
using DI.Reminder.BL.LoginService.Authentication;
using System.Security.Principal;
using DI.Reminder.BL.LoginService.UserProv;
using DI.Reminder.BL.UsersRepository;

namespace DI.Reminder.BL.BusinessRegistry
{
    public class BLRegistry:Registry
    {
        public BLRegistry()
        {
            For<IPrompts>().Use<Prompts>();
            For<IBLService>().Use<BLService>();
            For<ICategories>().Use<Categories>();
            For<IAuthentication>().Use<AccountAuthentication>();
            For<IPrincipal>().Use<UserProvider>();
            For<IUserRepository>().Use<UserRepository>();
        }
    }
}
