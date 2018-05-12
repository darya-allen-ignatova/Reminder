using System.Web.Mvc;
using DI.Reminder.BL.Service;

namespace DI.Reminder.Web.Controllers
{
    public class ServiceController : Controller
    {
        IBLService _blservice;
        public ServiceController(IBLService blservice)
        {
            _blservice = blservice;
        }
        // GET: Service
        public ActionResult Services()
        {
            return View(_blservice.Get());
        }
    }
}