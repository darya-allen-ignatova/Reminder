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
            IList<AdvertItem> list;
            try
            {
                list = client.GetItems();
            }
            catch(ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
            try
            {
                client.Close();
            }
            catch (CommunicationException)
            {
                
                client.Abort();
            }
            catch (TimeoutException)
            {
                client.Abort();
            }
            catch (Exception)
            {
                client.Abort();
                throw;
            }
            return list;
        }
        
    }
}
