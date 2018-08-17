using DI.Reminder.BL.Services;
using StructureMap;
using DI.Reminder.BL.CategoryStorage;
using DI.Reminder.BL.PromptStorage;
using DI.Reminder.BL.Cache;
using DI.Reminder.BL.LoginService.Authentication;
using System.Security.Principal;
using DI.Reminder.BL.LoginService.UserProv;
using DI.Reminder.BL.UsersService;
using DI.Reminder.BL.RoleStorage;

namespace DI.Reminder.Dependence.Registries
{
    public class BusinessRegistry:Registry
    {
        public BusinessRegistry()
        {
            For<ICacheService>().Use<CacheService>();
            For<IPrompt>().Use<Prompts>();
            For<IBLService>().Use<BLService>();
            For<ICategories>().Use<Categories>();
            For<IAuthentication>().Use<AccountAuthentication>();
            For<IPrincipal>().Use<UserProvider>();
            For<IUserService>().Use<UserService>();
            For<IRoles>().Use<Roles>();
        }
    }
}
