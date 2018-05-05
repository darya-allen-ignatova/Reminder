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
        private static readonly ILog logger; 
        static Logger()
        {
            XmlConfigurator.Configure();
            logger=LogManager.GetLogger("Logger");
        }
        
        public  void Debug(string _message)
        {
            logger.Debug("Debug: "+ _message);
        }
        public  void Error(string _message)
        {
            logger.Error("Error: "+ _message);
        }
        public void Fatal(string _message)
        {
            logger.Fatal("Fatal: "+_message);
        }
        public  void Warn(string _message)
        {
            logger.Warn("Warn: "+ _message);
        }


    }
}

