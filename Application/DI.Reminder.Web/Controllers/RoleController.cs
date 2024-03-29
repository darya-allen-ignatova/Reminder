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
            if (role == null)
                throw new ArgumentNullException();
            ModelState.Remove("ID");
            if (ModelState.IsValid)
            {
                _roles.InsertRole(role);
            }
            else
                RedirectToAction("HttpError500", "Error");
            return RedirectToAction("ShowAll");
        }


        public ActionResult Delete(int? id)
        {
            if(id==null)
                return RedirectToAction("HttpError404", "Error");
            var role = _roles.GetRole((int)id);
            if(role==null)
                return RedirectToAction("HttpError404", "Error");
            return View(role);
        }
        [HttpPost]
        public ActionResult Delete(Role role)
        {
            if (role == null)
                throw new ArgumentNullException();
             bool? flag=_roles.DeleteRole(role.ID);
            switch(flag)
            {
                case true:
                case null: return RedirectToAction("ShowAll");
                case false:ViewData["message"] = "Error.You can't delete role by the reason of availability user with this role"; return RedirectToAction("Delete", role.ID);
             }
            return RedirectToAction("ShowAll");
        }

        
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