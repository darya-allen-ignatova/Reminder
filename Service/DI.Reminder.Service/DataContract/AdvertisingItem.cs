
using System.Runtime.Serialization;

namespace DI.Reminder.Service.DataContract
{
    [DataContract]

    public class AdvertisingItem
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public byte[] Image { get; set; }
    }
}