using DI.Reminder.Common.CategoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.Data.CategoryDataBase
{
    public interface ICategoryRepository
    {
        int? GetCategoryID(string Name);
        int? GetCategoryParentID(string Name);
        IList<Category> GetCategories(int? id);
        void AddCategory(Category category);
        void DeleteCategory(int id);
        Category GetCategory(int id);
        IList<Category> GetAllCategories();
    }
}
