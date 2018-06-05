using DI.Reminder.BL.CategoryStorage;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Web.Filters;
using System.Web.Mvc;

namespace DI.Reminder.Web.Controllers
{
    [Admin]
    [Editor]
    public class CategoryController : Controller
    {
        ICategories _categoriesStorage;
        public CategoryController(ICategories categoriesStorage)
        {
            _categoriesStorage = categoriesStorage;
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Category category)
        {
            _categoriesStorage.InsertCategory(category);
            return RedirectToAction("ShowAllCategories");
        }
        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            _categoriesStorage.DeleteCategory(id);
            return View();
        }
        public ActionResult Details(int? id)
        {
            var categoryDetails = _categoriesStorage.GetCategory(id);
            if(categoryDetails!=null)
            return View(categoryDetails);
            else
                return RedirectToAction("HttpError404", "Error");
        }
        public ActionResult ShowAllCategories()
        {
            var allCategories = _categoriesStorage.GetAllCategories();
            if (allCategories != null)
                return View(allCategories);
            else
                return RedirectToAction("HttpError404", "Error");
        }
    }
}