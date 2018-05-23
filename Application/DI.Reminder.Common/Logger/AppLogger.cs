using log4net;
using log4net.Config;

namespace DI.Reminder.Common.Logger
{
    public class AppLogger:ILogger
    {
        private static readonly ILog logger; 
        static AppLogger()
        {
            XmlConfigurator.Configure();
            logger=LogManager.GetLogger("Logger");
        }
        
        public  void Debug(string _message)
        {
            logger.Debug("Debug "+ _message);
        }
        public  void Error(string _message)
        {
            logger.Error("Error "+ _message);
        }
        public void Fatal(string _message)
        {
            logger.Fatal("Fatal "+_message);
        }
        public  void Warn(string _message)
        {
            logger.Warn("Warn "+ _message);
        }


    }
}

