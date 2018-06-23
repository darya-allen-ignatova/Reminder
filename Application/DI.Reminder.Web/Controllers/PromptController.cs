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
        public ActionResult Search(string promptval, string categoryval=null , string dateval=null)
        {
            if (!(promptval != null || categoryval!=null || dateval!=null))
                throw new ArgumentNullException();
            var promptList = GetPromptsAfterSearch(promptval,categoryval,dateval);
            if(promptList==null || promptList.Count==0 )
            {
                return Json(new
                {
                    isEmpty = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(promptList, JsonRequestBehavior.AllowGet);
        }
        private IList<Prompt> GetPromptsAfterSearch(string promptval, string categoryval, string dateval)
        {
            IList<Prompt> promptList = new List<Prompt>();
            IList<Prompt> result = new List<Prompt>();
            IList<Prompt> listOfPromptName = null;
            IList<Prompt> listOfCategoryName = null;
            IList<Prompt> listOfDate = null;
            if (promptval != null&& promptval!="")
                listOfPromptName=_prompt.GetSearchingPrompts(UserID, 2, promptval);
            if (categoryval != null && categoryval!="0")
                listOfCategoryName = _prompt.GetSearchingPrompts(UserID, 1, categoryval);
            if (dateval != null && dateval!="")
                listOfDate = _prompt.GetSearchingPrompts(UserID, 3, dateval);
            if (listOfPromptName != null)
            {
                if (listOfCategoryName != null)
                {
                    foreach (var item in listOfPromptName)
                    {
                        if(listOfCategoryName.FirstOrDefault(g => g.ID == item.ID)!=null)
                        result.Add(listOfCategoryName.First(g => g.ID == item.ID));
                    }
                    if(listOfDate!=null)
                    {
                        foreach (var item in listOfDate)
                        {
                            if (result.FirstOrDefault(g => g.ID == item.ID) != null)
                                promptList.Add(result.First(g => g.ID == item.ID));
                        }
                        return promptList;
                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    if(listOfDate!=null)
                    {
                        foreach (var item in listOfDate)
                        {
                            if (listOfPromptName.FirstOrDefault(g => g.ID == item.ID) != null)
                                result.Add(listOfPromptName.First(g => g.ID == item.ID));
                        }
                        return result;
                    }
                    else
                    {
                        return listOfPromptName;
                    }
                }
            }
            else
            {
                if (listOfCategoryName != null)
                {
                    if (listOfDate != null)
                    {
                        foreach (var item in listOfCategoryName)
                        {
                            if (listOfDate.FirstOrDefault(g => g.ID == item.ID) != null)
                                result.Add(listOfDate.First(g => g.ID == item.ID));
                        }
                        return result;
                    }
                    else
                        return listOfCategoryName;
                }
                else
                    return listOfDate;
            }
        }






        public ActionResult Navigation(int? id = null)
        {
            IList<Prompt> _promptlist = _prompt.GetCategoryItemsByID(UserID,id);
            IList<Category> _categorylist = _getcategory.GetCategories(id);
            int? previousList =null;
            if (_promptlist.Count != 0)
            {
                previousList = _promptlist[0].Category.ParentID;
            }
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
            return View(promptViewModel);
        }
        [HttpPost]
        public void Add(Prompt prompt)
        {
            if (prompt == null)
                throw new ArgumentNullException();
            if (ModelState.IsValid)
            {
                _prompt.InsertPrompt(UserID, prompt);
            }
            else
                RedirectToAction("HttpError500", "Error");
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