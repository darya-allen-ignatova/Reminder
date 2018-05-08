using System.Web.Mvc;
using DI.Reminder.Common;

namespace DI.Reminder.Web.Controllers
{
    
    public class ErrorController : Controller
    {
        ILogger _logger;
        public ErrorController(ILogger logger)
        {
            if (_logger == null)
                _logger = logger;
        }
        // GET: Error
        public ActionResult HttpError404(string message)
        {
            //_logger.Error($"Error 404:\n{message}");
            return View();
        }
        public ActionResult HttpError500()
        {
            _logger.Error("500:\n");
            return View();
        }
        public ActionResult OtherErrors()
        {
            _logger.Error("Other:\n");
            return View();
        }
    }
}