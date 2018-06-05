using DI.Reminder.BL.RoleStorage;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Web.Filters;
using System.Web.Mvc;

namespace DI.Reminder.Web.Controllers
{
    [Admin]
    [Editor]
    public class RoleController : Controller
    {
        IRoles _roles;
        public RoleController(IRoles roles)
        {
            _roles = roles;
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Role role)
        {
            _roles.InsertRole(role);
            return RedirectToAction("ShowAllCategories");
        }
        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            _roles.DeleteRole(id);
            return View();
        }
       
        public ActionResult ShowAllRoles()
        {
            var allCategories = _roles.GetAllRoles();
            if (allCategories != null)
                return View(allCategories);
            else
                return RedirectToAction("HttpError404", "Error");
        }
    }
}