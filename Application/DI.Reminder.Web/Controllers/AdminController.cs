using DI.Reminder.BL.UsersRepository;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Web.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI.Reminder.Web.Controllers
{
    [Admin]
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
        public ActionResult DeleteUser(int? id)
        {
            Account account = _userRepository.GetUser(id);
            if (account == null)
                return RedirectToAction("HttpError404", "Error");
            return View(account);
        }
        [HttpPost]
        public ActionResult DeleteUser(Account account)
        {
            _userRepository.DeleteUser(account.ID);
            return RedirectToAction("GetUserList");
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
            return RedirectToAction("UserDetails");
        }




        public ActionResult GetUserList()
        {
            return View(_userRepository.GetUserList());
        }
        public ActionResult UserDetails(int? id)
        {
            Account account = _userRepository.GetUser(id);
            if (account == null)
                return RedirectToAction("HttpError404", "Error");
            return View(account);
        }
    }
}