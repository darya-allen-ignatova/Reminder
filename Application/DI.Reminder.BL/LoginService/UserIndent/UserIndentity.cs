using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.AccountDatabase;

namespace DI.Reminder.BL.LoginService.UserIndent
{
    public class UserIdentity:IIdentity
    {
        public Account _account { get; set; }
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
                return _account != null;
            }
        }

        public string Name
        {
            get
            {
                if (_account != null)
                    return _account.Login;
                return "anonym";
            }

        }
        
        public void Init(string login, IAccountRepository accountRepository)
        {
            if(!string.IsNullOrEmpty(login))
            {
                _account = accountRepository.GetAccount(login);
            }
        }
    }
}
