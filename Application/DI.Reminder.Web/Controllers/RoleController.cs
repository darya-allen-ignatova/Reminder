﻿using DI.Reminder.BL.RoleStorage;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Web.Filters;
using System;
using System.Web.Mvc;

namespace DI.Reminder.Web.Controllers
{
    [Admin]
    public class RoleController : Controller
    {
        private IRoles _roles;
        public RoleController(IRoles roles)
        {
            _roles = roles ?? throw new ArgumentNullException(nameof(roles));
        }


        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Role role)
        {
            _roles.InsertRole(role);
            return RedirectToAction("ShowAll");
        }


        public ActionResult Delete(int? id)
        {
            var role = _roles.GetRole(id);
            if(role==null)
                return RedirectToAction("HttpError404", "Error");
            return View(role);
        }
        [HttpPost]
        public ActionResult Delete(Role role)
        {
            _roles.DeleteRole(role.ID);
            return View();
        }



        //[OutputCache(CacheProfile = "cacheProfileForRoles")]
        public ActionResult ShowAll()
        {
            var allCategories = _roles.GetAllRoles();
            if (allCategories != null)
                return View(allCategories);
            else
                return RedirectToAction("HttpError404", "Error");
        }
    }
}