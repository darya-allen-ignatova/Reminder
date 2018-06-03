
﻿using DI.Reminder.Common.LoginModels;
﻿using System.Web;
using System.Security.Principal;
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
