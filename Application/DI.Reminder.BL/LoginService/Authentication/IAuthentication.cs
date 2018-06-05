
﻿using DI.Reminder.Common.LoginModels;
﻿using System.Web;
using System.Security.Principal;
using DI.Reminder.Data.RolesRepository;
using DI.Reminder.Data.AccountDatabase;
using DI.Reminder.Common.Logger;

namespace DI.Reminder.BL.LoginService.Authentication
{
    public interface IAuthentication
    {
        HttpContext httpContext { get; set; }
        Account Authentication(Account account, bool isPersistent = false);
        Account Login(string login);
        void Registration(Account account);
        void LogOut();
        IPrincipal CurrentUser { get; }
    }
}
