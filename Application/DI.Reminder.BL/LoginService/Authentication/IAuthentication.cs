using DI.Reminder.Common.LoginModels;
using System.Web;
using System.Security.Principal;

namespace DI.Reminder.BL.LoginService.Authentication
{
    public interface IAuthentication
    {
        HttpContext httpContext { get; set; }
        bool Authentication(Account account, bool isPersistent = false);
        void Registration(Account account);
        IPrincipal CurrentUser { get; }
    }
}
