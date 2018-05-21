using System.Collections.Generic;
using DI.Reminder.Data;
using System;
using DI.Reminder.Data.AppService;

namespace DI.Reminder.BL.Service
{
    public class BLService:IBLService
    {
        IDataService _dataservice;
        public BLService(IDataService dataservice)
        {
            _dataservice = dataservice;
        }
        public IEnumerable<ServiceItem> Get()
        {
            IList<AdvertItem> list = new List<AdvertItem>(); List<ServiceItem> _list;
            try
            {
                list = _dataservice.GetItems();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            _list = new List<ServiceItem>();
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
        public string ConvertByteArrToString(byte[] arr)
        {
            string base64 = Convert.ToBase64String(arr);
            string imgSrc = $"data:image;base64,{base64}";
            return imgSrc;
        }
    }
}