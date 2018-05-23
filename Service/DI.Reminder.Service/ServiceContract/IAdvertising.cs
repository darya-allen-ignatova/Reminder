using System.Collections.Generic;
using System.ServiceModel;
using DI.Reminder.Service.DataContract;


namespace DI.Reminder.Service.ServiceContract
{
    [ServiceContract]
    public interface IAdvertising
    {
        [OperationContract]
        IList<AdvertisingItem> GetItems();

    }
}
