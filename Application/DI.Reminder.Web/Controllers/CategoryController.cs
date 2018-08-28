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
            return View("Edit", categoryViewModel);
        }
        [HttpPost]
        public ActionResult Add(Category category)
        {
            if (category == null)
                throw new ArgumentNullException();
            ModelState.Remove("category.ID");
            if (ModelState.IsValid)
            {
                _categoriesStorage.InsertCategory(category);
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
            if (category != null)
                return View(category);
            else
                return RedirectToAction("HttpError404", "Error");
        }
        
        public ActionResult ShowAll()
        {
            var allCategories = _categoriesStorage.GetAllCategories();
            if (allCategories != null)
            {
                return View(allCategories);
            }
            else
            {
                return RedirectToAction("HttpError404", "Error");
            }
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
        public ActionResult Edit(CategoryViewModel categoryModel)
        {
            Category category = categoryModel.category;
            if (category == null)
                throw new ArgumentNullException();
            ModelState.Remove("category.ID");
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
    }
}