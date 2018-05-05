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
                string action;

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        action = "HttpError404";
                        break;
                    case 500:
                        action = "HttpError500";
                        break;
                    default:
                        action = "OtherErrors";
                        break;
                }
                Server.ClearError();
                Response.Redirect(String.Format("~/Error/{0}", action));
            }
            //if(httpException.GetHttpCode()!=404)
            //logger.Error(exception.Message+"\n"+exception.StackTrace);
        }
    }
    
}
