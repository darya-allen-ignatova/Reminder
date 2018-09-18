using System.Collections.Generic;
using DI.Reminder.Common.CategoryModel;

namespace DI.Reminder.BL.CategoryStorage
{
    public interface ICategories
    {
        IList<Category> GetCategories(int? id);
        void InsertCategory(Category category);
        void DeleteCategory(int id);
        IList<Category> GetAllCategories();
        Category GetCategory(int id);
        void EditCategory(Category category);
    }
}
