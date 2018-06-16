using System.Web.Mvc;
using DI.Reminder.BL.PromptStorage;
using DI.Reminder.BL.CategoryStorage;
using DI.Reminder.Web.Models;
using DI.Reminder.Common.PromptModel;
using System.Collections.Generic;
using System;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.BL.UsersService;

namespace DI.Reminder.Web.Controllers
{
    [Authorize]
    public class PromptController : Controller
    {
        private IUserService _userService;
        private IPrompt _prompt;
        private ICategories _getcategory;
        public PromptController(IPrompt prompt, ICategories getcategory, IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _prompt = prompt ?? throw new ArgumentNullException(nameof(prompt));
            _getcategory = getcategory ?? throw new ArgumentNullException(nameof(getcategory));
        }
        public ActionResult ShowCategoryList(int? id = null)
        {
            IList<Prompt> _promptlist = _prompt.GetCategoryItemsByID(UserID,id);
            IList<Category> _categorylist = _getcategory.GetCategories(id);
            ModelCategoriesWithPrompts modelCategoriesWithPrompts = new ModelCategoriesWithPrompts();
            if (_categorylist.Count != 0)
            {
                modelCategoriesWithPrompts.CategoryList = _categorylist;
                modelCategoriesWithPrompts.PromptList = _promptlist;
            }
            else
                return RedirectToAction("HttpError404", "Error");
            return View(modelCategoriesWithPrompts);
        }
        public ActionResult GetCategoryPrompts(int id)
        {
            IList<Prompt> _promptlist = _prompt.GetCategoryItemsByID(UserID, id);
            IList<Category> _categorylist = _getcategory.GetCategories(id);
            if((_categorylist!=null && _promptlist==null) || (_categorylist == null && _promptlist == null))
            {
                return Json(new
                {
                   isEmpty = true
                }, JsonRequestBehavior.AllowGet);
            }
            else if (_categorylist != null)
            {
                return Json(new
                {
                    ID = id,
                    isRedirect = true
                }, JsonRequestBehavior.AllowGet);
            }
            else 
            {
                return Json(_promptlist, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetItemsForSearch(int id, string value)
        {
            IList<Prompt> jsondata = _prompt.GetSearchingPrompts(UserID, id, value);
            if (jsondata == null || jsondata.Count == 0)
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
            Prompt prompt = _prompt.GetPromptDetails(UserID, ID);
            if (prompt == null)
                return RedirectToAction("HttpError404", "Error");
            return View(prompt);
        }


        public ActionResult Add()
        {
            var selectList = GetAllCategories();
            PromptViewModel promptViewModel = new PromptViewModel()
            {
                CategoryList = selectList
            };
            return View(promptViewModel);
        }
        [HttpPost]
        public void Add(Prompt prompt)
        {
            _prompt.InsertPrompt(UserID, prompt);
        }



        public ActionResult Delete(int? id)
        {
            if(id==null)
                return RedirectToAction("HttpError404", "Error");
            Prompt prompt = _prompt.GetPromptDetails(UserID, id);
            if (prompt == null )
                return RedirectToAction("HttpError404", "Error");
            return View(prompt);
        }
        [HttpPost]
        public ActionResult Delete(Prompt prompt)
        {
            _prompt.DeletePrompt(UserID, prompt.ID);
            return RedirectToAction("ShowCategoryList", new { id = 0 });
        }




        public ActionResult Edit(int? id)
        {
            if(id==null )
                return RedirectToAction("HttpError404", "Error");
            Prompt prompt = _prompt.GetPromptDetails(UserID, id);
            if (prompt == null)
                return RedirectToAction("HttpError404", "Error");
            var selectList = GetAllCategories();
            PromptViewModel promptViewModel = new PromptViewModel()
            {
                CategoryList = selectList,
                Prompt = prompt
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
                return _userService.GetUser(currentUser.Identity.Name).ID;
            }
        }


        
        private List<SelectListItem> GetAllCategories()
        {
            var listOfCategories = _getcategory.GetAllCategories();
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Root category"
            });
            for (int i = 0; i < listOfCategories.Count; i++)
            {
                selectList.Add(new SelectListItem()
                {
                    Value = listOfCategories[i].ID.ToString(),
                    Text = listOfCategories[i].Name
                });
            }
            return selectList;
        }
    }
}