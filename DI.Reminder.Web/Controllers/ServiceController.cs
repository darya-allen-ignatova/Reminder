using System.Web.Mvc;
using DI.Reminder.Web.AppService;


namespace DI.Reminder.Web.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Services()
        {
            var client = new AdvertisingClient();
            var advertising = client.GetItem();
            return View(advertising);
        }
    }
}