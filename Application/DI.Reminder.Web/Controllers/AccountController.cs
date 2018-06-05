using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DI.Reminder.BL.LoginService.Authentication;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.AccountDatabase;

namespace DI.Reminder.Web.Controllers
{
    public class AccountController : Controller
    {
        IAccountRepository _accountRepository;
        IAuthentication _authentication;
        public AccountController(IAuthentication authentication, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
        }
        // GET: Account
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(Account account)
        {
            List<Role> role = new List<Role>()
            {
                new Role()
                {
                    Name="User"
                }
            };
            account.Roles = role;
            _accountRepository.InsertAccount(account);
            _authentication.httpContext = System.Web.HttpContext.Current;
            _authentication.Registration(account);
            return RedirectToAction("Home", "Start");
        }
        public ActionResult Аuthentication()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Аuthentication(Account account)
        {
            _authentication.httpContext= System.Web.HttpContext.Current;
            _authentication.Authentication(account);
            return RedirectToAction("Home", "Start");
        }
    }
}