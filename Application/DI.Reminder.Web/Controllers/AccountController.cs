using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DI.Reminder.BL.LoginService.Authentication;
using DI.Reminder.BL.UsersService;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.RoleDatabase;

namespace DI.Reminder.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IAuthentication _authentication;
        private IRoleRepository _roleRepository;
        public AccountController(IAuthentication authentication, IUserService userService, IRoleRepository roleRepository)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public ActionResult LogOut()
        {
            Logout();
            return RedirectToAction("Home", "Start");
        }
        public ActionResult Registration()
        {
            return View("Edit", new Account());
        }

        [HttpPost]
        public ActionResult Registration(Account account)
        {
            if (ModelState.IsValid)
            {
                Role role = _roleRepository.GetRoleByName("User");
                account.Role = role;
                _userService.InsertUser(account);
                Logout();
                _authentication.httpContext = System.Web.HttpContext.Current;
                _authentication.Registration(account);
            }
            else
                RedirectToAction("HttpError500", "Error");
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account account)
        {
            _authentication.httpContext = System.Web.HttpContext.Current;
            if (!_authentication.Authentication(account))
            {
                account.Password = null;
                return View(account);
            }
            return RedirectToAction("Home", "Start");
        }
        public ActionResult Edit()
        {
            _authentication.httpContext = System.Web.HttpContext.Current;
            Account _account = _userService.GetUser(_authentication.CurrentUser.Identity.Name);
            if (_account == null)
                return RedirectToAction("Login");
            _account.Password = _account.Password.Replace(" ", string.Empty);
            _account.PasswordConfirm = _account.Password;
            return View(_account);
        }
        [HttpPost]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                _userService.EditUser(account);
            }
            else
                RedirectToAction("HttpError500", "Error");
            return RedirectToAction("Home", "Start");
        }
        private void Logout()
        {
            string[] myCookies = Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
        }
    }
}