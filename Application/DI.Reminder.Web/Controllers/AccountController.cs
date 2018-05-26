using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DI.Reminder.Common.LoginModels;

namespace DI.Reminder.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(Account account)
        {
            return View();
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