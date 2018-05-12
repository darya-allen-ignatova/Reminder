using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI.Reminder.Data.AppService;

namespace DI.Reminder.Data
{
    public class DataService:IDataService
    {
        AdvertisingClient client = new AdvertisingClient();
        public IList<AdvertItem> GetItems()
        {
            return client.GetItems();
        }
    }
}
