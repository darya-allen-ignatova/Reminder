﻿using DI.Reminder.BL.LoginService.UserProv;
using DI.Reminder.Common.Logger;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.AccountDatabase;
using DI.Reminder.Data.RolesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace DI.Reminder.BL.LoginService.Authentication
{
    public class AccountAuthentication:IAuthentication
    {
        string cookiename = "authcookie";
        IRoleRepository _roleRepository;
        IAccountRepository _accountRepository;
        ILogger _logger;
        public AccountAuthentication(IRoleRepository roleRepository, IAccountRepository accountRepository, ILogger logger)
        {
            _roleRepository = roleRepository;
            _accountRepository = accountRepository;
            _logger = logger;
        }
        public HttpContext httpContext { get; set; }

        public void Registration(Account account)
        {
            if (account != null)
            {
                CreateCookie(account);
            }
        }
        public Account Login(string userName, string Password, bool isPersistent)
        {
            Account account = _accountRepository.GetAccount(userName);
            if (account != null && account.Password==Password)
            {
                CreateCookie(account, isPersistent);
            }
            return account;
        }
        public Account GetAccount(string login)
        {
            return _accountRepository.GetAccount(login);
        }
        public Account Login(string login)
        {
            Account account = GetAccount(login);
            if (account != null)
            {
                CreateCookie(account);
            }
            return account;
        }

        private void CreateCookie(Account account, bool isPersistent = false)
        {
            var SerializeObj = JsonConvert.SerializeObject(account);
            var ticket = new FormsAuthenticationTicket(1,                
                  account.Login,
                  DateTime.Now,
                  DateTime.Now.Add(FormsAuthentication.Timeout),
                  isPersistent,
                  SerializeObj,
                  FormsAuthentication.FormsCookiePath);

            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var AuthCookie = new HttpCookie(cookiename)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };
            httpContext.Response.Cookies.Set(AuthCookie);
        }

        public void LogOut()
        {
            FormsAuthentication.SignOut();
        }

        private IPrincipal _currentUser;

        public IPrincipal CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    try
                    {
                        HttpCookie authCookie = HttpContext.Current.Request.Cookies.Get(cookiename);
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            _currentUser = new UserProvider(_roleRepository,_accountRepository, ticket.Name);
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
