﻿using DI.Reminder.BL.UsersRepository;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.AccountDatabase;
using DI.Reminder.Web.Filters;
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
        public ActionResult EditUser(int? id)
        {
            Account _account = _userRepository.GetUser(id);
            if (_account == null)
                return RedirectToAction("HttpError404", "Error");
            return View(_account);

        }
        [HttpPost]
        public ActionResult EditUser(Account account)
        {
            _userRepository.EditUser(account);
            return RedirectToAction("GetUserList");
        }

        [OutputCache(CacheProfile = "cacheProfileForUsers")]
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