using DI.Reminder.BL.RoleStorage;
using DI.Reminder.BL.UsersService;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Web.Filters;
using DI.Reminder.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DI.Reminder.Web.Controllers
{
    [Admin]
    public class AdminController : Controller
    {
        private IUserService _userService;
        private IRoles _roles;
        public AdminController(IUserService userService, IRoles roles)
        {
            _roles=roles?? throw new ArgumentNullException(nameof(roles));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        public ActionResult Home()
        {
            return View();
        }
    
        [HttpGet]
        public ActionResult AddUser()
        {
            ModelAccountWithRoles modelAccountWithRoles = new ModelAccountWithRoles()
            {
                AllRoles = GetRoles(null)
            };
            return View("EditUser",modelAccountWithRoles);
        }
        [HttpPost]
        public ActionResult AddUser(Account account)
        {
            if (account == null)
                throw new ArgumentNullException();
            ModelState.Remove("account.Roles[0].Name");
            if (ModelState.IsValid)
            {
                _userService.InsertUser(account);
            }
            else
                RedirectToAction("HttpError500", "Error");
            return RedirectToAction("UserList");
        }
        [HttpGet]
        public ActionResult DeleteUser(int? id)
        {
            if(id==null)
                return RedirectToAction("HttpError404", "Error");
            Account account = _userService.GetUser((int)id);
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
            Account _account = _userService.GetUser((int)id);
            _account.Password = _account.Password.Replace(" ", string.Empty);
            _account.PasswordConfirm = _account.Password;
            if (_account == null)
                return RedirectToAction("HttpError404", "Error");
            var roleList = GetRoles(_account.Roles[0]);
            ModelAccountWithRoles modelAccountWithRoles = new ModelAccountWithRoles()
            {
                Account = _account,
                AllRoles = roleList
            };
            return View(modelAccountWithRoles);

        }
        [HttpPost]
        public ActionResult EditUser(Account account)
        {
            if (account == null)
                throw new ArgumentNullException();
            ModelState.Remove("account.Roles[0].Name");
            if (ModelState.IsValid)
            {
                _userService.EditUser(account);
            }
            else
                RedirectToAction("HttpError500", "Error");
            return RedirectToAction("UserList");
        }
        
        public ActionResult UserList()
        {
            return View(_userService.GetUserList());
        }


        public ActionResult UserDetails(int? id)
        {
            if (id == null)
                return RedirectToAction("HttpError404", "Error");
            Account account = _userService.GetUser((int)id);
            if (account == null)
                return RedirectToAction("HttpError404", "Error");
            return View(account);
        }
        private IList<SelectListItem> GetRoles(Role role)
        {
            IList<SelectListItem> selectList = new List<SelectListItem>();
            var roleList = _roles.GetAllRoles();
            
            foreach(var item in roleList)
            {
                bool roleSelect= false;
                if (role != null && item.ID == role.ID)
                    roleSelect = true;
                selectList.Add(new SelectListItem()
                {
                    Selected=roleSelect,
                    Value=item.ID.ToString(),
                    Text=item.Name
                });
            }
            return selectList;
        }
    }
}