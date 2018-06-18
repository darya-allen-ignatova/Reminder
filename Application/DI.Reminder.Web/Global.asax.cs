using DI.Reminder.BL.LoginService.Authentication;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DI.Reminder.Web.DependencyResolution;
using System.Data.SqlClient;

namespace DI.Reminder.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        IAuthentication authentication = IoC.Initialize().GetInstance<IAuthentication>();
       
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();
            String connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            System.Web.Caching.SqlCacheDependencyAdmin.EnableNotifications(connStr);
            System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(connStr, "Prompts");
            System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(connStr, "Categories");
            System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(connStr, "Users");
            System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(connStr, "Roles");
            SqlDependency.Start(connStr);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception == null)
                return;
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
                Response.Redirect(String.Format("~/Error/{0}/?message={1}", _action, exception.Message));
            }

        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            authentication.httpContext = System.Web.HttpContext.Current;
            HttpContext.Current.User = authentication.CurrentUser;
        }


    }



}