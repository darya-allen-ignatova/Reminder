using System.Web.Mvc;
using DI.Reminder.BL.PromptStorage;
using DI.Reminder.BL.Categories;
using DI.Reminder.Web.Models;
using DI.Reminder.Common.PromptModel;
using System.Collections.Generic;

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
        }
        // GET: Prompt
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult ShowCategoryList(int? id=null)
        {
            PromptViewModel ins = new PromptViewModel();
            IList<Common.PromptModel.Prompt> _promptlist = _getprompt.GetCategoryItemsByID(id);
            if (_promptlist == null || id == 0 || _promptlist.Count == 0)
                {
                    ins = new PromptViewModel()
                    {
                        CategoryList = _getcategory.GetCategories(id),
                        PromptList = _promptlist
                    };

                }
                else
                {
                    ins = new PromptViewModel()
                    {
                        CategoryList = _getcategory.GetCategories(_getcategory.GetCategoryID(_promptlist[0].Category)),
                        PromptList = _promptlist
                    };


                }
            if (ins.CategoryList.Count == 0)
                    return RedirectToAction("HttpError404", "Error");
            return View(ins);
            
        }
        public ActionResult Details(int? ID)
        {
            Common.PromptModel.Prompt prompt = _getprompt.GetPromptDetails(ID);
            if(prompt==null)
                return RedirectToAction("HttpError404", "Error");
            return View(prompt);
        }
       
    }
}