using DI.Reminder.Data.AdvertisingService;
using System.Collections.Generic;
using DI.Reminder.Common.ServiceModel;

namespace DI.Reminder.Data.DService
{
    public interface IDataService
    {
        IList<ServiceItem> GetItems();
    }
}
