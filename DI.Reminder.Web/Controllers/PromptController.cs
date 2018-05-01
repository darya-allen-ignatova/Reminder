using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DI.Reminder.BL;
using DI.Reminder.Common;

namespace DI.Reminder.Web.Controllers
{
    public class PromptController : Controller
    {
        IPrompt prompt;
        ILogger logger;
        public PromptController(IPrompt _prompt, ILogger _logger)
        {
            prompt = _prompt;
            if(logger==null)
            logger = _logger;
        }
        // GET: Prompt
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Show()
        {
            return View();
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
        public ActionResult PromptDetails(int? id)
        {
            //Логирование
            //logger.Debug(typeof(PromptController));
            return View(prompt.GetPrompt(id));
        }
    }
}