using System.Web;
using System.Web.Mvc;

namespace DI.Reminder.Web.Filters
{
    public class Editor:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var currentUser = HttpContext.Current.User;

            if (currentUser != null)
            {
                return currentUser.IsInRole("Editor") || currentUser.IsInRole("Admin");
            }
            return false;
        }
    }
}