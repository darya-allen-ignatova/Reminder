<<<<<<< HEAD
﻿using DI.Reminder.Common.LoginModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
=======
﻿using System.Web;
using DI.Reminder.Common.LoginModels;
using System.Security.Principal;
>>>>>>> re-7

namespace DI.Reminder.BL.LoginService.Authentication
{
    public interface IAuthentication
    {
        HttpContext httpContext { get; set; }

        Account Login(string login, string password, bool isPersistent);

<<<<<<< HEAD
        Account Login(string login);
        void Registration(Account account);
=======
        Account Login(string login, bool isPersistent);
        void Login(Account account);
>>>>>>> re-7

        void LogOut();

        IPrincipal CurrentUser { get; }
    }
}
