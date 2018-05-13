using System.Collections.Generic;
using System.Web.Mvc;
using DI.Reminder.BL;
using DI.Reminder.BL.Repository;

namespace DI.Reminder.Web.Controllers
{
    public class PromptController : Controller
    {
        private readonly IPrompt prompt;
        IPromptRepository _promptrep;
        public PromptController(IPrompt _prompt, IPromptRepository promptrep)
        {
            prompt = _prompt;
            _promptrep = promptrep;
        }
        // GET: Prompt
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Show(int id=0)
        {
            return View(_promptrep.GetCategoryItemsByID(id));
        }
        public ActionResult PromptDetails(int ID)
        {
            return View(_promptrep.GetPromptDetails(ID));
        }
        //public ActionResult ShowCategoryItems(int id)
        //{
        //    return View(_promptrep.GetCategoryItemsByID(id));
        //}
    }
}