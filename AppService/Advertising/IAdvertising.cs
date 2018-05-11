using System.Runtime.Serialization;
using System.ServiceModel;


namespace AppService
{
    [ServiceContract]
    public interface IAdvertising
    {
        [OperationContract]
        AdvertItem GetItem();
    }
}
