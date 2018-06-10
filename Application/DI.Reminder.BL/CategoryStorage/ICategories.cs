using System.Collections.Generic;
using DI.Reminder.Common.CategoryModel;

namespace DI.Reminder.BL.CategoryStorage
{
    public interface ICategories
    {
        IList<Category> GetCategories(int? id);
        int? GetCategoryParentID(string categoryName);
        void InsertCategory(Category category);
        void DeleteCategory(int? id);
        IList<Category> GetAllCategories();
        Category GetCategory(int? id);
        int? GetCategoryIDByName(string Name);
        void EditCategory(Category category);
    }
}
