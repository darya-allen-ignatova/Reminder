using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DI.Reminder.Data.AppService;

namespace DI.Reminder.Data
{
    public class DataService:IDataService
    {
        public IList<AdvertItem> GetItems()
        {
            AdvertisingClient client = new AdvertisingClient();
            IList<AdvertItem> list= client.GetItems();
            try
            {
                client.Close();
            }
            catch (CommunicationException e)
            {
                
                client.Abort();
            }
            catch (TimeoutException e)
            {
                client.Abort();
            }
            catch (Exception e)
            {
                client.Abort();
                throw;
            }
            return list;
        }
        
    }
}
