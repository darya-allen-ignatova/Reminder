using DI.Reminder.Common.CategoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.Data.DataBase
{
    public interface ICategoryRepository
    {
        int? GetCategoryID(string Name);
        IList<Category> GetCategories(int? id);
    }
}
