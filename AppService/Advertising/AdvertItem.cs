using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace AppService
{
    [DataContract]

    public class AdvertItem
    {
        Image _image;
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public byte[] ImageAsBytes
        {
            get
            {
                using (var ms = new MemoryStream())
                {
                    _image.Save(ms, ImageFormat.Bmp);
                     return ms.ToArray();
                }
            }
            
            set {
                MemoryStream ms = new MemoryStream(value, 0, value.Length);
                ms.Write(value, 0, value.Length);
                _image = Image.FromStream(ms, true);
            }
        }
         

    }
}