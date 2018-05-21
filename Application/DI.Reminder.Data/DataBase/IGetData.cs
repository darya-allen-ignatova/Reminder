using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.Data.DataBase
{
    public interface IGetData
    {
        IList<DataPrompt> GetItems(int? id);
        DataPrompt GetPrompt(int? id);
        IList<DataCategory> GetCategories(int? id);
        int? GetCategoryID(string Name);
    }
}
