using System.Web.Mvc;

namespace DI.Reminder.Web.Controllers
{
    public class StartController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }
    }
}