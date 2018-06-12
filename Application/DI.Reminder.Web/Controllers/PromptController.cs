﻿using System.Web.Mvc;
using DI.Reminder.BL.PromptStorage;
using DI.Reminder.BL.CategoryStorage;
using DI.Reminder.Web.Models;
using DI.Reminder.Common.PromptModel;
using System.Collections.Generic;
using System;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Data.CategoryDataBase;
using DI.Reminder.BL.UsersRepository;
using System.Linq;

namespace DI.Reminder.Web.Controllers
{
    [Authorize]
    public class PromptController : Controller
    {
        private IUserRepository _userRepository;
        private IPrompt _prompt;
        private ICategories _getcategory;
        public PromptController(IPrompt prompt, ICategories getcategory, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _prompt = prompt;
            _getcategory = getcategory;
            if (_prompt == null || _getcategory == null)
                throw new ArgumentNullException();
        }
        [OutputCache(CacheProfile ="cacheProfileForCategories")]
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
        public ActionResult GetItemsForSearch(int id, string value)
        {
            IList<Prompt> jsondata = _prompt.GetSearchingPrompts(UserID, id, value);
            if (jsondata == null || jsondata.Count==0)
            {
                return Json(new
                {
                    ID = id,
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
            var CategoryList = GetAllCategories();
            List<SelectListItem> selectList = new List<SelectListItem>();
            for(int i=0; i< CategoryList.Count; i++)
            {
                selectList.Add(new SelectListItem()
                {
                    Value = CategoryList[i].ID.ToString(),
                    Text = CategoryList[i].Name
                });
            }
            PromptViewModel promptViewModel = new PromptViewModel()
            {
                CategoryList = selectList
            };
            return View(promptViewModel);
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
            var CategoryList = GetAllCategories();
            Prompt prompt = _prompt.GetPromptDetails(UserID,id);
            if(id == null || prompt==null  )
                 return RedirectToAction("HttpError404", "Error");
            int? ID =_getcategory.GetCategoryIDByName(prompt.Category);
            List<SelectListItem> selectList = new List<SelectListItem>();
            for (int i = 0; i < CategoryList.Count; i++)
            {
                selectList.Add(new SelectListItem()
                {
                    Value = CategoryList[i].ID.ToString(),
                    Text = CategoryList[i].Name
                });
                selectList[i].Selected = (selectList[i].Text == prompt.Category);
            }

            PromptViewModel promptViewModel = new PromptViewModel()
            {
                CategoryList = selectList,
                Prompt = prompt,
                Property = ID.ToString()
            };
            
            return View(promptViewModel);
        }
        [HttpPost]
        public ActionResult Edit(Prompt prompt)
        {
            _prompt.EditPrompt(prompt);
            return RedirectToAction("ShowCategoryList", new { id = 0 });
        }
        private int UserID
        {
           get
            {
                var currentUser = System.Web.HttpContext.Current.User;
                return _userRepository.GetUser(currentUser.Identity.Name).ID;
            }
        }
        private IList<Category> GetAllCategories()
        {
            return _getcategory.GetAllCategories();
        }
    }
}