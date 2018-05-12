using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DI.Reminder.Service.Advertising
{
    public class Advertisings : IAdvertising
    {
        public IList<AdvertItem> GetItems()
        {
            AdvertRepository rep = new AdvertRepository();

            IList<AdvertItem> list = rep.GetItems();
            
            if (list == null)
            {
                throw new ArgumentNullException();
                 
            }
            return list;

        }
    }
}