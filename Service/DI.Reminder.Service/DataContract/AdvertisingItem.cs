using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

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