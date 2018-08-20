using System.Collections.Generic;
using DI.Reminder.Common.ServiceModel;

namespace DI.Reminder.BL.Services
{
    public interface IBLService
    {
        IList<ServiceItem> Get();
    }
}
