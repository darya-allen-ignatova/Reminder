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
            _blservice = blservice?? throw new ArgumentNullException(nameof(blservice));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        [ChildActionOnly]
        public ActionResult Advertising()
        {
            var items = GetServiceItems();
            if(items.Count==1)
            {
                ViewBag.Flag = true;
            }
            return View(items);
        }
        
        private IList<ServiceItem> GetServiceItems()
        {
            IList<ServiceItem> advertising = _blservice.Get();
            if (advertising == null)
            {
                return GetDefaultServiceItem();
            }
            return advertising;
        }

        private IList<ServiceItem> GetDefaultServiceItem()
        {
            return new List<ServiceItem>()
                {
                    new ServiceItem()
                    {
                        ID=0,
                        Title="We are against advertising. Please enjoy the site",
                        Url=null,
                        Image="/Images/0.jpg"
                    }
                };
            }
        }
    }
