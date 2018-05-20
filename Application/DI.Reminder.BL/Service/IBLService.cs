using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.BL.Service
{
    public interface IBLService
    {
        IEnumerable<ServiceItem> Get();
    }
}
