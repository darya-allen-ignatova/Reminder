using DI.Reminder.Data.AppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.Data
{
    public interface IDataService
    {
        IList<AdvertItem> GetItems();
    }
}
