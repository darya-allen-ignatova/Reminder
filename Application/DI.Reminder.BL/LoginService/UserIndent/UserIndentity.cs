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
        private IAccountRepository _accountRepository;
        public UserIndentity(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
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
        
        public void Init(string login)
        {
            if(!string.IsNullOrEmpty(login))
            {
                account = _accountRepository.GetAccount(login);
            }
        }
    }
}
