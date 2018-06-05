using DI.Reminder.BL.CategoryStorage;
using DI.Reminder.Common.CategoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI.Reminder.Web.Controllers
{
    public class CategoryController : Controller
    {
        ICategories _categoriesStorage;
        public CategoryController(ICategories categoriesStorage)
        {
            _categoriesStorage = categoriesStorage;
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            _categoriesStorage.InsertCategory(category);
            return RedirectToAction("ShowAllCategories");
        }
        public ActionResult DeleteCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteCategory(int? id)
        {
            _categoriesStorage.DeleteCategory(id);
            return View();
        }
        public ActionResult ShowAllCategories()
        {
            return View();
        }
    }
}