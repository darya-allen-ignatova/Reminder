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
                _logger = logger;
            if ( _logger == null)
                throw new ArgumentNullException();
        }
        // GET: Error
        public ActionResult HttpError404(string message)
        {
            _logger.Error("404:\n" + message);
            return View();
        }
        public ActionResult HttpError500(string message)
        {
            _logger.Error("500:\n"+message);
            return View();
        }
        public ActionResult OtherErrors(string message)
        {
            _logger.Error("Other:\n"+message);
            return View();
        }
    }
}