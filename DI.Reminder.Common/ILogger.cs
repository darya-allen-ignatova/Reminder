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
        //ILog For(object LoggedObject);
        ILog For(Type ObjectType);
        void Debug(Type ObjectType);
    }
}
