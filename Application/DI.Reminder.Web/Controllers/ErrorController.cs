using System;
using System.Web.Mvc;
using DI.Reminder.Common.Logger;

namespace DI.Reminder.Web.Controllers
{
    
    public class ErrorController : Controller
    {
        private ILogger _logger;
        public ErrorController(ILogger logger)
        {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public ActionResult HttpError404(string message)
        {
            return View();
        }
        public ActionResult HttpError500(string message)
        {
            _logger.Error("500:   "+message);
            return View();
        }
        public ActionResult OtherErrors(string message)
        {
            _logger.Error("Other:   "+message);
            return View();
        }
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}