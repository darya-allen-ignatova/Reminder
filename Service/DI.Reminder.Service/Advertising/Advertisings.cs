using System;
using System.Collections.Generic;
using DI.Reminder.Service.DataContract;
using DI.Reminder.Service.ServiceContract;

namespace DI.Reminder.Service.Advertising
{
    public class Advertisings : IAdvertising
    {
        public IList<AdvertisingItem> GetItems()
        {
            AdvertisingData data = new AdvertisingData();

            IList<AdvertisingItem> list = data.GetItems();
            
            if (list == null)
            {
                throw new ArgumentNullException();
            }
            return list;

        }
    }
}