using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using DI.Reminder.BL.LoginService.UserProv;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.AccountDatabase;
using DI.Reminder.Data.RolesRepository;
using DI.Reminder.Common.Logger;

namespace DI.Reminder.BL.LoginService.Authentication
{
    public class AccountAuthentication : IAuthentication
    {
        private const string cookieName = "authcookie";
        private IAccountRepository _accountRepository;
        private IRoleRepository _roleRepository;
        private IPrincipal _currentUser;
        private ILogger _logger;

        public AccountAuthentication(IAccountRepository accountRepository, IRoleRepository roleRepository, ILogger logger)
        {
            _logger = logger;
            _roleRepository = roleRepository;
            _accountRepository = accountRepository;
        }
        public HttpContext httpContext {get;set;}


        public Account Login(string login, string password, bool isPersistent=false)
        {
            Account account = _accountRepository.GetAccount(login);
            if(account != null && account.Password==password )
            {
                CreateCookie(login, isPersistent);
            }
            return account;
        }

        public Account Login(string login, bool isPersistent = false)
        {
            Account account = _accountRepository.GetAccount(login);
            if(account!=null)
            {
                CreateCookie(login, isPersistent);
            }
            return account;
        }
        public void Login(Account account)
        {
            if (account != null)
            {
                LogOut();
                CreateCookie(account.Login, false);
            }
        }
        private void CreateCookie(string login, bool isPersistent)
        {
            var ticket = new FormsAuthenticationTicket(login, isPersistent, 3600);
            
            var encTicket = FormsAuthentication.Encrypt(ticket);
            
            var AuthCookie = new HttpCookie(cookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };
            httpContext.Response.Cookies.Set(AuthCookie);
        }
        public void LogOut()
        {
            var httpCookie = httpContext.Response.Cookies[cookieName];
            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
            }
        }
        public IPrincipal CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    try
                    {
                        HttpCookie authCookie =httpContext.Request.Cookies.Get(cookieName);
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            _currentUser = new UserProvider(_roleRepository,_accountRepository,ticket.Name);
                        }
                        else
                        {
                            _currentUser = new UserProvider(null, null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Failed authentication: " + ex.Message);
                        _currentUser = new UserProvider(null, null, null);
                    }
                }
                return _currentUser;
            }
        }
    }
}
