using DI.Reminder.BL.LoginService.Authentication;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DI.Reminder.Web.DependencyResolution;
using DI.Reminder.Data.RolesRepository;
using DI.Reminder.Data.AccountDatabase;
using DI.Reminder.Common.Logger;

namespace DI.Reminder.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        IRoleRepository roleRepository = IoC.Initialize().GetInstance<IRoleRepository>();
        IAccountRepository accountrepository = IoC.Initialize().GetInstance<IAccountRepository>();
        ILogger logger = IoC.Initialize().GetInstance<ILogger>();
        IAuthentication authentication = IoC.Initialize().GetInstance<IAuthentication>();
       
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();
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
        //protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        //{
        //    IAuthentication authentication = new AccountAuthentication();
        //    Context.User = authentication.CurrentUser;
        //    //Context.User.IsInRole("User");
        //    string[] roles = new string[] { "User" };
        //    if (Context.User != null)
        //        Context.User = new GenericPrincipal(Context.User.Identity, roles);

        //}
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext.Current.User = authentication.CurrentUser;
            //if (cookie != null)
            //{
            //var decryptedCookie = FormsAuthentication.Decrypt(cookie.Value);
            //var _account = JsonConvert.DeserializeObject<Account>(decryptedCookie.UserData);
            //IAuthentication 
            //HttpContext.Current.User = _account;
        }

    }



}