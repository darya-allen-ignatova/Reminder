using System.Web.Mvc;
using DI.Reminder.BL.PromptStorage;
using DI.Reminder.BL.Categories;
using DI.Reminder.Web.Models;
using DI.Reminder.Common.PromptModel;
using System.Collections.Generic;
using System;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Data.DataBase;

namespace DI.Reminder.Web.Controllers
{
    public class PromptController : Controller
    {
        private IPrompt _prompt;
        private IGetCategories _getcategory;
        private ICategoryRepository _categoryRepository;
        public PromptController(IPrompt prompt, IGetCategories getcategory, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _prompt = prompt;
            _getcategory = getcategory;
            if (_prompt == null || _getcategory == null)
                throw new ArgumentNullException();
        }
        // GET: Prompt
        public ActionResult Home()
        {
            return View();
        }
        //public ActionResult ShowCategoryList(int? id=null)
        //{
        //    PromptViewModel Model = GetViewModel(id);
        //    if (Model.CategoryList.Count == 0 || Model.CategoryList==null)
        //            return RedirectToAction("HttpError404", "Error");
        //    return View(Model);
            
        //}
        public ActionResult ShowCategoryList(int? id = null)
        {
            IList<Category> _categorylist = _getcategory.GetCategories(id);
            if(_categorylist.Count!=0)
            return View(_categorylist);
            else
                return RedirectToAction("HttpError404", "Error");

        }
        public ActionResult GetCategoryPrompts(int id)
        {
            IList<Prompt> jsondata = _prompt.GetCategoryItemsByID(id);
            if (jsondata.Count == 0)
            {
                return Json(new
                {
                    ID=id,
                    isRedirect = true
                }, JsonRequestBehavior.AllowGet);
            }
            
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        private PromptViewModel GetViewModel(int? id)
        {
            PromptViewModel promptModel = new PromptViewModel();
            IList<Prompt> _promptlist = _prompt.GetCategoryItemsByID(id); ;
            if (_promptlist == null || id == 0 || _promptlist.Count == 0)
            {
                promptModel = new PromptViewModel()
                {
                    CategoryList = _getcategory.GetCategories(id),
                    PromptList = _promptlist
                };

            }
            else
            {
                promptModel = new PromptViewModel()
                {
                    CategoryList = _getcategory.GetCategories(_getcategory.GetCategoryParentID(_promptlist[0].Category)),
                    PromptList = _promptlist
                };

            }
            return promptModel;
        }
        
        public ActionResult Details(int? ID)
        {
            Common.PromptModel.Prompt prompt = _prompt.GetPromptDetails(ID);
            if(prompt==null)
                return RedirectToAction("HttpError404", "Error");
            return View(prompt);
        }
       public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public void Add(Prompt prompt)
        {
            _prompt.InsertPrompt(prompt, _categoryRepository);
        }
       
        public ActionResult Delete(int? id)
        {
            Prompt prompt = _prompt.GetPromptDetails(id);
            if (prompt == null || id == null)
                return RedirectToAction("HttpError404", "Error");
            return View(prompt);
        }
        [HttpPost]
        public ActionResult Delete(Prompt prompt)
        {
            _prompt.DeletePrompt(prompt.ID);
            return RedirectToAction("ShowCategoryList", new { id = 0 });
        }
        public ActionResult Edit(int? id)
        {
            Prompt prompt = _prompt.GetPromptDetails(id);
            if(prompt==null || id == null)
                 return RedirectToAction("HttpError404", "Error");
            return View(prompt);
        }
        [HttpPost]
        public ActionResult Edit(Prompt prompt)
        {
            return View();
        }
    }
}