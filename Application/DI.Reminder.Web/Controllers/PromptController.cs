using System.Web.Mvc;
using DI.Reminder.BL.PromptStorage;
using DI.Reminder.BL.Categories;
using DI.Reminder.Web.Models;
using DI.Reminder.Common.PromptModel;
using System.Collections.Generic;
using System;

namespace DI.Reminder.Web.Controllers
{
    public class PromptController : Controller
    {
        private IPrompt _getprompt;
        private IGetCategories _getcategory;
        public PromptController(IPrompt getprompt, IGetCategories getcategory)
        {
            _getprompt = getprompt;
            _getcategory = getcategory;
            if (_getprompt == null || _getcategory == null)
                throw new ArgumentNullException();
        }
        // GET: Prompt
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult ShowCategoryList(int? id=null)
        {
            PromptViewModel Model = GetViewModel(id);
            if (Model.CategoryList.Count == 0 || Model.CategoryList==null)
                    return RedirectToAction("HttpError404", "Error");
            return View(Model);
            
        }
        private PromptViewModel GetViewModel(int? id)
        {
            PromptViewModel promptModel = new PromptViewModel();
            IList<Prompt> _promptlist = _getprompt.GetCategoryItemsByID(id); ;
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
                    CategoryList = _getcategory.GetCategories(_getcategory.GetCategoryID(_promptlist[0].Category)),
                    PromptList = _promptlist
                };

            }
            return promptModel;
        }
        
        public ActionResult Details(int? ID)
        {
            Common.PromptModel.Prompt prompt = _getprompt.GetPromptDetails(ID);
            if(prompt==null)
                return RedirectToAction("HttpError404", "Error");
            return View(prompt);
        }
       public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Prompt prompt)
        {
            return RedirectToAction("ShowCategoryList", new { id = 0 });
        }
        public ActionResult Delete(int id)
        {

            return View();
        }
        [HttpPost]
        public ActionResult Delete(Prompt prompt)
        {

            return RedirectToAction("ShowCategoryList", new { id = 0 });
        }

    }
}