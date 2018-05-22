using System.Collections.Generic;
using System.ServiceModel;


namespace DI.Reminder.Service.Advertising
{
    [ServiceContract]
    public interface IAdvertising
    {
        [OperationContract]
        IList<AdvertItem> GetItems();

    }
}
