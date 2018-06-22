using DI.Reminder.BL.CategoryStorage;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Web.Filters;
using DI.Reminder.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DI.Reminder.Web.Controllers
{
    [Editor]
    public class CategoryController : Controller
    {
        private ICategories _categoriesStorage;
        public CategoryController(ICategories categoriesStorage)
        {
            _categoriesStorage = categoriesStorage ?? throw new ArgumentNullException(nameof(categoriesStorage));
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
            if (categoryModel == null)
                throw new ArgumentNullException();
            if (ModelState.IsValid)
            {
                _categoriesStorage.InsertCategory(GetFromModel(categoryModel));
            }
            else
                RedirectToAction("HttpError500", "Error");
            return RedirectToAction("ShowAll");
        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            if (category == null)
                throw new ArgumentNullException();
            _categoriesStorage.DeleteCategory(category.ID);
            return RedirectToAction("ShowAll");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("HttpError404", "Error");
            var category = _categoriesStorage.GetCategory((int)id);
            CategoriesModel categoryModel = GetModel(category);
            if (categoryModel != null)
                return View(categoryModel);
            else
                return RedirectToAction("HttpError404", "Error");
        }

            public ActionResult Details(int? id)
        {
            if(id==null)
                return RedirectToAction("HttpError404", "Error");
            var categoryDetails = _categoriesStorage.GetCategory((int)id);
            if (categoryDetails != null)
                return View(categoryDetails);
            else
                return RedirectToAction("HttpError404", "Error");
        }


        //[OutputCache(CacheProfile = "cacheProfileForCategories")]
        public ActionResult ShowAll()
        {
            var allCategories = _categoriesStorage.GetAllCategories();
            IList<CategoriesModel> categoriesModel = new List<CategoriesModel>();
            if (allCategories != null)
            {
                for (int i = 0; i < allCategories.Count; i++)
                {
                    categoriesModel.Add(GetModel(allCategories[i]));
                }
                return View(categoriesModel);
            }
            else
            {
                return RedirectToAction("HttpError404", "Error");
            }
        }
        private CategoriesModel GetModel(Category category)
        {
            CategoriesModel categoriesModel = null;
            if (category != null)
            {
                string parentCategory = null;
                if (category.ParentID != null)
                    parentCategory = _categoriesStorage.GetCategory((int)category.ParentID).Name;

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
        public ActionResult Edit(int? ID)
        {
            if(ID==null)
                return RedirectToAction("HttpError404", "Error");
            var CategoryList = GetCategoriesList();
            var categoryDetails = _categoriesStorage.GetCategory((int)ID);
            if (categoryDetails == null)
                return RedirectToAction("HttpError404", "Error");
            CategoryViewModel categoryViewModel = new CategoryViewModel()
            {
                category =categoryDetails,
                AllCategories = CategoryList
            };
            return View(categoryViewModel);

        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (category == null)
                throw new ArgumentNullException();
            if (ModelState.IsValid)
            {
                _categoriesStorage.EditCategory(category);
            }
            else
                RedirectToAction("HttpError500", "Error");
            return RedirectToAction("ShowAll");
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
        private Category GetFromModel(CategoryViewModel categoryModel)
        {
            return new Category()
            {
                ID = categoryModel.category.ID,
                Name = categoryModel.category.Name,
                ParentID = categoryModel.category.ParentID
            };
        }
    }
}