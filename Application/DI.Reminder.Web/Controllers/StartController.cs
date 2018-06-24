using System.Web.Mvc;

namespace DI.Reminder.Web.Controllers
{
    [AllowAnonymous]
    public class StartController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }
    }
}