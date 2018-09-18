using DI.Reminder.Common.CategoryModel;
using System.Collections.Generic;

namespace DI.Reminder.Data.CategoryDataBase
{
    public interface ICategoryRepository
    {
        IList<Category> GetCategories(int? id);
        void AddCategory(Category category);
        void DeleteCategory(int id);
        Category GetCategory(int id);
        IList<Category> GetAllCategories();
        void EditCategory(Category category);
    }
}
