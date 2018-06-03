using DI.Reminder.BL.UsersRepository;
using DI.Reminder.Common.LoginModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI.Reminder.Web.Controllers
{
    public class AdminController : Controller
    {
        IUserRepository _userRepository;
        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }



        [HttpPost]
        public ActionResult AddUser(Account account)
        {
            _userRepository.InsertUser(account);
            return RedirectToAction("GetUserList");
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }


        [HttpGet]
        public ActionResult DeleteUser()
        {
            return View();

        }
        [HttpPost]
        public ActionResult DeleteUser(Account account)
        {
            _userRepository.DeleteUser(account.ID);
            return View();
        }



        [HttpGet]
        public ActionResult EditUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditUser(Account account)
        {
            _userRepository.EditUser(account);
            return View();
        }




        public ActionResult GetUserList()
        {
            return View(_userRepository.GetUserList());
        }
    }
}