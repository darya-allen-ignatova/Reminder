using System.ComponentModel;
using System.Web.Mvc;

namespace DI.Reminder.Web.Controllers
{
    [Editor]
    public class EditorController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }
    }
}