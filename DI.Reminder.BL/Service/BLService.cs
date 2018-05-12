using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DI.Reminder.Data;

namespace DI.Reminder.BL.Service
{
    public class BLService:IBLService
    {
        IDataService _dataservice;
        public BLService(IDataService dataservice)
        {
            dataservice = _dataservice;
        }
        public IEnumerable<ServiceItem> Get()
        {
            var list = _dataservice.GetItems();
            List<ServiceItem> _list = new List<ServiceItem>();
            for(int i = 0; i<3; i++)
            {
                _list[i] = new ServiceItem { ID = list[i].ID, Title = list[i].Title, Url = list[i].Url, Image = list[i].Image };
            }
            return _list;
        }
    }
}