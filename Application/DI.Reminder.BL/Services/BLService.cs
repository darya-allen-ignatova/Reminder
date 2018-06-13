using System.Collections.Generic;
using System;
using DI.Reminder.Data.DService;
using DI.Reminder.Common.ServiceModel;

namespace DI.Reminder.BL.Services
{
    public class BLService:IBLService
    {
        private IDataService _dataservice;
        public BLService(IDataService dataservice)
        {
            _dataservice = dataservice ?? throw new ArgumentNullException(nameof(dataservice));
        }
        public IEnumerable<ServiceItem> Get()
        {
            IList<ServiceItem> list = new List<ServiceItem>();
            try
            {
                list = _dataservice.GetItems();
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            return list;
        }
       
    }
}