using System.Web.Mvc;
using DI.Reminder.BL.PromptStorage;
using DI.Reminder.BL.CategoryStorage;
using DI.Reminder.Web.Models;
using DI.Reminder.Common.PromptModel;
using System.Collections.Generic;
using System;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.BL.UsersService;
using System.Linq;

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


        
        public ActionResult Searching()
        {
            SearchModel searchModel = new SearchModel()
            {
                Categories = GetAllCategories()
            };
            searchModel.Categories[0].Text = string.Empty;
            return View(searchModel);
        }
        public ActionResult Search(string promptval=null, string categoryval=null , string dateval=null)
        {
            if (!(promptval != null || categoryval!=null || dateval!=null))
                throw new ArgumentNullException();
            var promptList =_prompt.GetSearchingPrompts(UserID,promptval,categoryval,dateval);
            if(promptList==null || promptList.Count==0 )
            {
                return Json(new
                {
                    isEmpty = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(promptList, JsonRequestBehavior.AllowGet);
        }
        
        

        public ActionResult Navigation(int? id = null)
        {
            IList<Prompt> _promptlist = _prompt.GetCategoryItemsByID(UserID,id);
            IList<Category> _categorylist = _getcategory.GetCategories(id);
            int? previousList =null;
            try
            {
                previousList = _getcategory.GetCategory((int)_categorylist[0].ParentID).ParentID;
            }
            catch
            { }
            ModelCategoriesWithPrompts modelCategoriesWithPrompts = new ModelCategoriesWithPrompts();
            if (_categorylist.Count != 0)
            {
                modelCategoriesWithPrompts.CategoryList = _categorylist;
                modelCategoriesWithPrompts.PromptList = _promptlist;
                modelCategoriesWithPrompts.previousListID = previousList;
            }
            else
                return RedirectToAction("HttpError404", "Error");
            return View(modelCategoriesWithPrompts);
        }
        public ActionResult GetCategoryPrompts(int? id)
        {
            if(id==null)
                return RedirectToAction("HttpError404", "Error");
            IList<Prompt> _promptlist = _prompt.GetCategoryItemsByID(UserID, id);
            IList<Category> _categorylist = _getcategory.GetCategories((int)id);
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


        

        public ActionResult Details(int? ID)
        {
            if(ID==null)
                return RedirectToAction("HttpError404", "Error");
            Prompt prompt = _prompt.GetPromptDetails(UserID, (int)ID);
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
            return View("Edit",promptViewModel);
        }
        [HttpPost]
        public ActionResult Add(Prompt prompt)
        {
            if (prompt == null)
                throw new ArgumentNullException();
            if (ModelState.IsValid)
            {
                _prompt.InsertPrompt(UserID, prompt);
            }
            else
                RedirectToAction("HttpError500", "Error");
            return RedirectToAction("Navigation");
        }



        public ActionResult Delete(int? id)
        {
            if(id==null)
                return RedirectToAction("HttpError404", "Error");
            Prompt prompt = _prompt.GetPromptDetails(UserID, (int)id);
            if (prompt == null )
                return RedirectToAction("HttpError404", "Error");
            return View(prompt);
        }
        [HttpPost]
        public ActionResult Delete(Prompt prompt)
        {
            if (prompt == null)
                throw new ArgumentNullException();
             _prompt.DeletePrompt(UserID, prompt.ID); 
            return RedirectToAction("Navigation", new { id = 0 });
        }




        public ActionResult Edit(int? id)

        {
            if(id==null )
                return RedirectToAction("HttpError404", "Error");
            Prompt prompt = _prompt.GetPromptDetails(UserID, (int)id);
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
            if (prompt == null)
                throw new ArgumentNullException();
            ModelState.Remove("prompt.Category.Name");
            if (ModelState.IsValid)
            {
                _prompt.EditPrompt(prompt);
            }
            else
                RedirectToAction("HttpError500", "Error");
            return RedirectToAction("Navigation", new { id = 0 });
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