using System.Collections.Generic;
using System.Web.Mvc;
using DI.Reminder.BL;
using DI.Reminder.Common;

namespace DI.Reminder.Web.Controllers
{
    public class PromptController : Controller
    {
        private readonly IPrompt prompt;
        public PromptController(IPrompt _prompt)
        {
            prompt = _prompt;
           
        }
        // GET: Prompt
        public ActionResult Home()
        {

            return View();
        }
        public ActionResult Show()
        {
            return View(prompt.GetAll());
        }
        public ActionResult CategoryDetails(string _category)
     {
           IEnumerable<Prompt> _list = prompt.GetList(_category);
           if (Request.IsAjaxRequest())
           {
                return PartialView(prompt.GetList(_category));
           }
           return View(_list);
        }
        public ActionResult PromptDetails(int? ID)
        {
            return View(prompt.GetPrompt(ID));
        }
    }
}