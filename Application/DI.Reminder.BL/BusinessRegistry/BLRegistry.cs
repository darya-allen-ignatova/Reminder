using DI.Reminder.BL.Services;
using StructureMap;
using DI.Reminder.BL.CategoryStorage;
using DI.Reminder.BL.PromptStorage;
using DI.Reminder.BL.CachedRepository;
using DI.Reminder.BL.LoginService.Authentication;
using System.Security.Principal;
using DI.Reminder.BL.LoginService.UserProv;
using DI.Reminder.BL.UsersRepository;
using DI.Reminder.BL.RoleStorage;
using DI.Reminder.BL.CachedRepository.Prompts;
using DI.Reminder.BL.CachedRepository.Categories;

namespace DI.Reminder.BL.BusinessRegistry
{
    public class BLRegistry:Registry
    {
        public BLRegistry()
        {
            For<IPrompt>().Use<Prompts>();
            For<IBLService>().Use<BLService>();
            For<IPromptCache>().Use<PromptCache>();
            For<ICategoryCache>().Use<CategoryCache>();
            For<ICategories>().Use<Categories>();
            For<IAuthentication>().Use<AccountAuthentication>();
            For<IPrincipal>().Use<UserProvider>();
            For<IUserRepository>().Use<UserRepository>();
            For<IRoles>().Use<Roles>();
        }
    }
}
