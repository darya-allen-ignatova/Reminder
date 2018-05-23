using System.Collections.Generic;
using System.Web.Mvc;
using DI.Reminder.BL.Services;
using DI.Reminder.Common.Logger;
using DI.Reminder.Common.ServiceModel;

namespace DI.Reminder.Web.Controllers
{
    public class ServiceController : Controller
    {
        private IBLService _blservice;
        private ILogger _logger;
        public ServiceController(IBLService blservice, ILogger logger)
        {
            _blservice = blservice;
            _logger = logger;
            
        }
        // GET: Service
        [ChildActionOnly]
        public ActionResult Advertising()
        {
            IEnumerable<ServiceItem> advertising = _blservice.Get();
            if (advertising == null)
            {
                advertising = new List<ServiceItem>()
                {
                    new ServiceItem()
                    {
                        ID=0,
                        Title="Против рекламы",
                        Url=null,
                        Image="/Images/0.jpg"
                    }
                };
            }
            return View(advertising);
        }
    }
}