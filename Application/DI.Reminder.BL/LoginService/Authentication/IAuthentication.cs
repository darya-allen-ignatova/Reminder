using System.Web;
using DI.Reminder.Common.LoginModels;
using System.Security.Principal;

namespace DI.Reminder.BL.LoginService.Authentication
{
    public interface IAuthentication
    {
        HttpContext httpContext { get; set; }

        Account Login(string login, string password, bool isPersistent);

        Account Login(string login, bool isPersistent);
        void Login(Account account);

        void LogOut();

        IPrincipal CurrentUser { get; }
    }
}
