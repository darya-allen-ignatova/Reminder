using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace DI.Reminder.Web.Filters
{
    public class Admin : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
           var currentUser = HttpContext.Current.User;

           if (currentUser != null)
           {
               return currentUser.IsInRole("Admin");
           }

           return false;
       }
    }
}