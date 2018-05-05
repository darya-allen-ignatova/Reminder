using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.Common
{
    public interface ILogger
    {
        void  Error(string _message);
        void Debug(string _message);
        void Warn(string _message);
        void Fatal(string _message);
    }
}
