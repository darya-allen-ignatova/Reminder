using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

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
                ID=1,Title="Books", Url="https://oz.by/",Image=GetImageByteArray(@"/Images/IMG_8839.jpg")
            },
            new AdvertItem
            {
                ID=2,Title="TV series", Url="http://seasonvar.ru/",Image=GetImageByteArray(@"/Images/_______________.png")
            },
            new AdvertItem
            {
                ID=3,Title="Programming Site", Url="https://metanit.com/",Image=GetImageByteArray(@"/Images/6LNrDidlao8.jpg")
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
            {
                AdvertItem _advert = _list.FirstOrDefault(f => f.ID == RandomItem());
                list.Add(new AdvertItem()
                {
                    ID = _advert.ID, Title = _advert.Title, Url = _advert.Url, Image = _advert.Image
                });
            }
            return list;
        }
        public int RandomItem()
        {
            Random rnd = new Random();
            return rnd.Next(1, _list.Count);
        }
        public byte[] GetBytesFromImage(string path)
        {
            string physicalPath = HttpContext.Current.Server.MapPath(path);
            return File.ReadAllBytes(physicalPath);
        }
        public byte[] GetImageByteArray(string imagePath)
        {
            string currentPath = HostingEnvironment.ApplicationPhysicalPath;
            var byteArray = File.ReadAllBytes(currentPath + imagePath);
            return byteArray;

        }
    }
}