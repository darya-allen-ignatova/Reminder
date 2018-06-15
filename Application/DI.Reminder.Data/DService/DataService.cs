using System;
using System.Collections.Generic;
using System.ServiceModel;
using DI.Reminder.Data.AdvertisingService;
using DI.Reminder.Common.ServiceModel;
using DI.Reminder.Service.DataContract;
using DI.Reminder.Common.Logger;

namespace DI.Reminder.Data.DService
{
    public class DataService:IDataService
    {
        ILogger _logger;
        public DataService(ILogger logger)
        {
            _logger = logger;
        }
        public IList<ServiceItem> GetItems()
        {
            AdvertisingClient client = new AdvertisingClient();
            IList<AdvertisingItem> list = null;
            try
            {
                list = client.GetItems();
            }
            catch(ArgumentNullException argnullexc)
            {
                _logger.Error("ServiceException:   "+ argnullexc.Message);
            }
            catch(Exception ex)
            {
                _logger.Error("ServiceException:   " + ex.Message);
            }
            try
            {
                client.Close();
            }
            catch (CommunicationException comExc)
            {
                _logger.Error("ServiceException:   " + comExc.Message);
                client.Abort();
            }
            catch (TimeoutException timeExc)
            {
                _logger.Error("ServiceException:   " + timeExc.Message);
                client.Abort();
            }
            catch (Exception ex)
            {
                _logger.Error("ServiceException:   " + ex.Message);
                client.Abort();
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
