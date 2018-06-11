using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DI.Reminder.BL.LoginService.Authentication;
using DI.Reminder.BL.UsersRepository;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.AccountDatabase;

namespace DI.Reminder.Web.Controllers
{
    public class AccountController : Controller
    {
        IUserRepository _userRepository;
        IAuthentication _authentication;
        public AccountController(IAuthentication authentication, IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
        }
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
            _userRepository.InsertUser(account);
            _authentication.httpContext = System.Web.HttpContext.Current;
            _authentication.Registration(account);
            return RedirectToAction("Home", "Start");
        }
        public ActionResult LogOn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogOn(Account account)
        {
            _authentication.httpContext= System.Web.HttpContext.Current;
            _authentication.Authentication(account);
            return RedirectToAction("Home", "Start");
        }
        public ActionResult EditUser()
        {
            _authentication.httpContext = System.Web.HttpContext.Current;
            Account _account = _userRepository.GetUser(_authentication.CurrentUser.Identity.Name);
            if(_account==null)
                return RedirectToAction("LogOn");
            _account.Password = _account.Password.Replace(" ", string.Empty);
            _account.PasswordConfirm = _account.Password;
            return View(_account);
            
        }
        [HttpPost]
        public ActionResult EditUser(Account account)
        {
            _userRepository.EditUser(account);
            return RedirectToAction("Home", "Start");
        }
    }
}