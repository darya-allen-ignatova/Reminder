using DI.Reminder.BL.Services;
using StructureMap;
using DI.Reminder.BL.Categories;
using DI.Reminder.BL.PromptStorage;
using DI.Reminder.BL.LoginService.Authentication;
using System.Security.Principal;
using DI.Reminder.BL.LoginService.UserProv;

namespace DI.Reminder.BL.BusinessRegistry
{
    public class BLRegistry:Registry
    {
        public BLRegistry()
        {
            For<IPrompt>().Use<Prompts>();
            For<IBLService>().Use<BLService>();
            For<IGetCategories>().Use<GetCategory>();
            For<IAuthentication>().Use<AccountAuthentication>();
            For<IPrincipal>().Use<UserProvider>();
        }
    }
}
