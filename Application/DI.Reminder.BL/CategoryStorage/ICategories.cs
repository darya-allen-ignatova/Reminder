using System.Collections.Generic;
using DI.Reminder.Common.CategoryModel;

namespace DI.Reminder.BL.CategoryStorage
{
    public interface IGetCategories
    {
        IList<Category> GetCategories(int? id);
        int? GetCategoryParentID(string categoryName);
    }
}
