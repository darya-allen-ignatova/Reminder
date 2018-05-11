using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Drawing;

namespace AppService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Advertising" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Advertising.svc или Advertising.svc.cs в обозревателе решений и начните отладку.
    public class Advertising : IAdvertising
    {
        private static readonly List<AdvertItem> _list = new List<AdvertItem>()
        {
            new AdvertItem
            {
                ID=1,Title="Books", Url="https://oz.by/"
            },
            new AdvertItem
            {
                ID=2,Title="TV series", Url="http://seasonvar.ru/"
            },
            new AdvertItem
            {
                ID=3,Title="Programming Site", Url="https://metanit.com/"
            }
        };
        public IEnumerable<AdvertItem> Items
        {
            get { return _list; }
        }
        public AdvertItem GetItem()
        {
            AdvertItem advertising = _list.FirstOrDefault(f => f.ID == RandomItem());
            return advertising;
        }
        public int RandomItem()
        {
            Random rnd = new Random();
            return rnd.Next(1, _list.Count);
        }
        
    }
}
