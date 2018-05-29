using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            _accountRepository = accountRepository;
            _authentication = authentication;
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
            account.roles = role;
            _accountRepository.InsertAccount(account);
            _authentication.httpContext = System.Web.HttpContext.Current;
            _authentication.Login(account);
            return RedirectToAction("Home", "Prompt");
        }
        public ActionResult Аuthentication()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Аuthentication(Account account)
        {
            return View();
        }
    }
}