using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DI.Reminder.Common;

namespace DI.Reminder.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();

        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();
            HttpException httpException = exception as HttpException;
            if (httpException != null)
            {
                string _action;
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        _action = "HttpError404";
                        break;
                    case 500:
                        _action = "HttpError500";
                        break;
                    default:
                        _action = "OtherErrors";
                        break;
                }
                Server.ClearError();
                Response.Redirect($"~/Error/{_action}");
                //Response.RedirectToRoute(new { controller = "Error", action = _action }, new { message = exception.Message + "\n" + exception.StackTrace } );
                
                
            }
            
        }
    }
    
}
