using System.Web.Mvc;
using DI.Reminder.BL.Service;
using DI.Reminder.Common;

namespace DI.Reminder.Web.Controllers
{
    public class ServiceController : Controller
    {
        IBLService _blservice;ILogger _logger;
        public ServiceController(IBLService blservice, ILogger logger)
        {
            _blservice = blservice;
            _logger = logger;
            
        }
        // GET: Service
        public ActionResult Services()
        {
            var advertising = _blservice.Get();
            if (advertising == null)
                return Redirect("Error");
            return View(_blservice.Get());
        }
    }
}