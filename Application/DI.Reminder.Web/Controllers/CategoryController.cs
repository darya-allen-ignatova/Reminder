using DI.Reminder.BL.CategoryStorage;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Web.Filters;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DI.Reminder.Web.Controllers
{
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
        public ActionResult Add(CategoriesModel categoryModel)
        {
            Category category = null;
            if (categoryModel != null)
            {
                category = new Category() {
                    ID=categoryModel.ID,
                    Name=categoryModel.Name,
                    ParentID=_categoriesStorage.GetCategoryIDByName(categoryModel.ParentCategory)
                };
                _categoriesStorage.InsertCategory(category);
            }
            return RedirectToAction("ShowAllCategories");
        }
        public ActionResult Delete(int? id)
        {
            var category = _categoriesStorage.GetCategory(id);
            CategoriesModel categoriesModel = GetModel(category);
            if (categoriesModel != null)
                return View(categoriesModel);
            else
                return RedirectToAction("HttpError404", "Error");
        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            _categoriesStorage.DeleteCategory(category.ID);
            return View();
        }
        public ActionResult Details(int? id)
        {
            var categoryDetails = _categoriesStorage.GetCategory(id);
            CategoriesModel categoriesModel = GetModel(categoryDetails);
            if (categoriesModel != null)
                return View(categoriesModel);
            else
                return RedirectToAction("HttpError404", "Error");
        }
        [OutputCache(CacheProfile = "cacheProfileForCategories")]
        public ActionResult ShowAll()
        {
            var allCategories = _categoriesStorage.GetAllCategories();
            IList<CategoriesModel> categoriesModel = new List<CategoriesModel>();
            if (allCategories != null)
            {
                for (int i = 0; i < allCategories.Count; i++)
                {
                    string parentCategory = null;
                    if (allCategories[i].ParentID != null)
                        parentCategory = _categoriesStorage.GetCategory(allCategories[i].ParentID).Name;
                    categoriesModel.Add(new CategoriesModel()
                    {
                        ID = allCategories[i].ID,
                        Name = allCategories[i].Name,
                        ParentCategory = parentCategory
                    });
                }
            
            return View(categoriesModel);
            }
            else
                return RedirectToAction("HttpError404", "Error");
        }
        public ActionResult Edit(int? ID)
        {
            var categoryDetails = _categoriesStorage.GetCategory(ID);
            CategoriesModel categoriesModel = GetModel(categoryDetails);
            if (categoriesModel != null)
                return View(categoriesModel);
            else
                return RedirectToAction("HttpError404", "Error");
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            _categoriesStorage.EditCategory(category);
            return RedirectToAction("ShowAll");
        }

        private CategoriesModel GetModel(Category category)
        {
            CategoriesModel categoriesModel = null;
            if (category != null)
            {
                string parentCategory = null;
                if (category.ParentID != null)
                    parentCategory = _categoriesStorage.GetCategory(category.ParentID).Name;

                categoriesModel = new CategoriesModel()
                {
                    ID = category.ID,
                    Name = category.Name,
                    ParentCategory = parentCategory
                };
                return categoriesModel;
            }
            return null;
        }
    }
}