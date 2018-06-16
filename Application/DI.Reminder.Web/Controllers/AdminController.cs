using DI.Reminder.BL.UsersService;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Web.Filters;
using System;
using System.Web.Mvc;

namespace DI.Reminder.Web.Controllers
{
    [Admin]
    public class AdminController : Controller
    {
        private IUserService _userService;
        public AdminController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }


        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(Account account)
        {
            if (account == null)
                throw new ArgumentNullException();
            _userService.InsertUser(account);
            return RedirectToAction("UserList");
        }
        


        [HttpGet]
        public ActionResult DeleteUser(int? id)
        {
            if(id==null)
                return RedirectToAction("HttpError404", "Error");
            Account account = _userService.GetUser(id);
            if (account == null)
                return RedirectToAction("HttpError404", "Error");
            return View(account);
        }
        [HttpPost]
        public ActionResult DeleteUser(Account account)
        {
            if (account == null)
                throw new ArgumentNullException();
            _userService.DeleteUser(account.ID);
            return RedirectToAction("UserList");
        }



        [HttpGet]
        public ActionResult EditUser(int? id)
        {
            if(id==null)
                return RedirectToAction("HttpError404", "Error");
            Account _account = _userService.GetUser(id);
            _account.Password = _account.Password.Replace(" ", string.Empty);
            if (_account == null)
                return RedirectToAction("HttpError404", "Error");
            return View(_account);

        }
        [HttpPost]
        public ActionResult EditUser(Account account)
        {
            if (account == null)
                throw new ArgumentNullException();
            _userService.EditUser(account);
            return RedirectToAction("UserList");
        }



        //[OutputCache(CacheProfile = "cacheProfileForUsers")]
        public ActionResult UserList()
        {
            return View(_userService.GetUserList());
        }


        public ActionResult UserDetails(int? id)
        {
            if (id == null)
                return RedirectToAction("HttpError404", "Error");
            Account account = _userService.GetUser(id);
            if (account == null)
                return RedirectToAction("HttpError404", "Error");
            return View(account);
        }
    }
}