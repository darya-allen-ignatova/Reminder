using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.Common
{
    public class Logger:ILogger
    {

        static Logger()
        {
            XmlConfigurator.Configure();
        }

        //public ILog For(object LoggedObject)
        //{
        //    if (LoggedObject != null)
        //        return For(LoggedObject.GetType());
        //    else
        //        return For(null);
        //}

        public ILog For(Type ObjectType)
        {
            if (ObjectType != null)
                return LogManager.GetLogger(ObjectType.Name);
            else
                return LogManager.GetLogger(string.Empty);
        }
        public void Debug(Type ObjectType)
        {
            For(ObjectType).Debug("Debug Error: ");
        }
    }
}

