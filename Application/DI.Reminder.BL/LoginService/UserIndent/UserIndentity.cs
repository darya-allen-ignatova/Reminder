using System;
using System.Security.Principal;
using DI.Reminder.BL.LoginService.Authentication;
using DI.Reminder.BL.LoginService.UserProv;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.AccountDatabase;

namespace DI.Reminder.BL.LoginService.UserIndent
{
    public class UserIndentity:IIdentity
    {
        public Account account { get; set; }
        public string AuthenticationType
        {
            get
            {
                return typeof(Account).ToString();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return account != null;
            }
        }

        public string Name
        {
            get
            {
                if (account != null)
                    return account.Login;
                return "anonym";
            }

        }
        
        public void Init(string login, IAccountRepository accountRepository)
        {
            if(!string.IsNullOrEmpty(login))
            {
                account = accountRepository.GetAccount(login);
            }
        }
    }
}
