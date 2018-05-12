using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DI.Reminder.Service.Advertising
{
    public class AdvertRepository
    {
        List<AdvertItem> _list;
        public AdvertRepository()
        {
            _list = new List<AdvertItem>()
        {
            new AdvertItem
            {
                ID=1,Title="Books", Url="https://oz.by/",Image=GetBytesFromImage(@"~Images/IMG_8839.jpg")
            },
            new AdvertItem
            {
                ID=2,Title="TV series", Url="http://seasonvar.ru/",Image=GetBytesFromImage(@"~Images/_______________.jpg")
            },
            new AdvertItem
            {
                ID=3,Title="Programming Site", Url="https://metanit.com/",Image=GetBytesFromImage(@"~Images/6LNrDidlao8.jpg")
            }
        };
        }
        public IEnumerable<AdvertItem> Items
        {
            get { return _list; }
        }
        public IList<AdvertItem> GetItems()
        {
            List<AdvertItem> list = new List<AdvertItem>();
            for (int i = 0; i < 3; i++)
                list[i] = _list.FirstOrDefault(f => f.ID == RandomItem());
            return list;
        }
        public int RandomItem()
        {
            Random rnd = new Random();
            return rnd.Next(1, _list.Count);
        }
        public byte[] GetBytesFromImage(string path)
        {
            //get the physical path
            string physicalPath = HttpContext.Current.Server.MapPath(path);
            //read and return byte[]
            return File.ReadAllBytes(physicalPath);
        }
    }
}