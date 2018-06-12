using DI.Reminder.BL.CategoryStorage;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Web.Filters;
using DI.Reminder.Web.Models;
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
            var allCategories = GetCategoriesList();
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.AllCategories = allCategories;
            return View(categoryViewModel);
        }
        [HttpPost]
        public ActionResult Add(CategoryViewModel categoryModel)
        {
            Category category = null;
            if (categoryModel != null)
            {
                category = new Category() {
                    ID=categoryModel.category.ID,
                    Name=categoryModel.category.Name,
                    ParentID=categoryModel.category.ParentID
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
            var CategoryList = GetCategoriesList();
            var categoryDetails = _categoriesStorage.GetCategory(ID);
            if (categoryDetails == null)
                return RedirectToAction("HttpError404", "Error");
            CategoryViewModel categoryViewModel = new CategoryViewModel()
            {
                category = new Category()
                {
                    ID = categoryDetails.ID,
                    Name = categoryDetails.Name,
                    ParentID = categoryDetails.ParentID
                },
                AllCategories=CategoryList
            };
            return View(categoryViewModel);
               
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
        private List<SelectListItem> GetCategoriesList()
        {
            var allCategories = _categoriesStorage.GetAllCategories();
            var selectlist = new List<SelectListItem>();
            selectlist.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Root category"
            });
            for (int i = 0; i < allCategories.Count; i++)
            {
                selectlist.Add(new SelectListItem()
                {
                    Value = allCategories[i].ID.ToString(),
                    Text = allCategories[i].Name
                });
            }
            return selectlist;
        }
    }
}