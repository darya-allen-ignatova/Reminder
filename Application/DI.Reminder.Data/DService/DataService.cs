using System;
using System.Collections.Generic;
using System.ServiceModel;
using DI.Reminder.Data.AdvertisingService;
using DI.Reminder.Common.ServiceModel;
using DI.Reminder.Service.DataContract;

namespace DI.Reminder.Data.DService
{
    public class DataService:IDataService
    {
        public IList<ServiceItem> GetItems()
        {
            AdvertisingClient client = new AdvertisingClient();
            IList<AdvertisingItem> list;
            try
            {
                list = client.GetItems();
            }
            catch(ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
            catch
            {
                throw new Exception();
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
            List<ServiceItem>_list = new List<ServiceItem>();
            for (int i = 0; i < 3; i++)
            {
                _list.Add(new ServiceItem()
                {
                    ID = list[i].ID,
                    Title = list[i].Title,
                    Url = list[i].Url,
                    Image = ConvertByteArrToString(list[i].Image)
                });
            }
            return _list;
        }
        private string ConvertByteArrToString(byte[] arr)
        {
            string base64 = Convert.ToBase64String(arr);
            string imgSrc = $"data:image;base64,{base64}";
            return imgSrc;
        }

    }
}
