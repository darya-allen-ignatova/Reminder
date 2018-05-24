using System;
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
            if (_blservice == null || _logger == null)
                throw new ArgumentNullException();
        }

        // GET: Service
        [ChildActionOnly]
        public ActionResult Advertising()
        {
            return View(GetServiceItems());
        }


        private IEnumerable<ServiceItem> GetServiceItems()
        {
            IEnumerable<ServiceItem> advertising = _blservice.Get();
            if (advertising == null)
            {
                return GetDefaultServiceItem();
            }
            return advertising;
        }
        private IEnumerable<ServiceItem> GetDefaultServiceItem()
        {
            return new List<ServiceItem>()
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
        }
    }
