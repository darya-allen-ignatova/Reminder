using DI.Reminder.Common.LoginModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DI.Reminder.BL.LoginService.Authentication
{
    public interface IAuthentication
    {
        HttpContext httpContext { get; set; }

        Account Login(string login, string password, bool isPersistent);

        Account Login(string login);
        void Registration(Account account);

        void LogOut();

        IPrincipal CurrentUser { get; }
    }
}
