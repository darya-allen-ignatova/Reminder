using System.Collections.Generic;
using System.Runtime.Serialization;
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
