using System.Web.Mvc;
using DI.Reminder.BL.PromptStorage;
using DI.Reminder.BL.CategoryStorage;
using DI.Reminder.Web.Models;
using DI.Reminder.Common.PromptModel;
using System.Collections.Generic;
using System;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Data.CategoryDataBase;
using DI.Reminder.Web.Filters;
using System.Web;
using DI.Reminder.BL.UsersRepository;

namespace DI.Reminder.Web.Controllers
{
    [Authorize]
    public class PromptController : Controller
    {
        private IUserRepository _userRepository;
        private IPrompts _prompt;
        private ICategories _getcategory;
        private ICategoryRepository _categoryRepository;
        public PromptController(IPrompts prompt, ICategories getcategory, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _prompt = prompt;
            _getcategory = getcategory;
            if (_prompt == null || _getcategory == null)
                throw new ArgumentNullException();
        }
        
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
            IList<Prompt> jsondata = _prompt.GetCategoryItemsByID(UserID,id);
            if(jsondata==null)
            {
                return Json(new
                {
                    message = "There are no prompts", isEmpty = true  },
                JsonRequestBehavior.AllowGet);
            }
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
        
        public ActionResult Details(int? ID)
        {
            Common.PromptModel.Prompt prompt = _prompt.GetPromptDetails(UserID,ID);
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
            _prompt.InsertPrompt(UserID,prompt);
        }
        public ActionResult Delete(int? id)
        {
            Prompt prompt = _prompt.GetPromptDetails(UserID,id);
            if (prompt == null || id == null)
                return RedirectToAction("HttpError404", "Error");
            return View(prompt);
        }
        [HttpPost]
        public ActionResult Delete(Prompt prompt)
        {
            _prompt.DeletePrompt(UserID,prompt.ID);
            return RedirectToAction("ShowCategoryList", new { id = 0 });
        }
        
        public ActionResult Edit(int? id)
        {
            Prompt prompt = _prompt.GetPromptDetails(UserID,id);
            if(prompt==null || id == null)
                 return RedirectToAction("HttpError404", "Error");
            return View(prompt);
        }
        [HttpPost]
        public ActionResult Edit(Prompt prompt)
        {
            return View();
        }
        private int UserID
        {
           get
            {
                var currentUser = System.Web.HttpContext.Current.User;
                return _userRepository.GetUser(currentUser.Identity.Name).ID;
            }
        }
    }
}