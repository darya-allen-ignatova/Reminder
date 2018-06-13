using DI.Reminder.Common.LoginModels;
using System.Web;
using System.Security.Principal;

namespace DI.Reminder.BL.LoginService.Authentication
{
    public interface IAuthentication
    {
        HttpContext httpContext { get; set; }
        Account Authentication(Account account, bool isPersistent = false);
        Account Registration(Account account);
        void LogOut();
        IPrincipal CurrentUser { get; }
    }
}
