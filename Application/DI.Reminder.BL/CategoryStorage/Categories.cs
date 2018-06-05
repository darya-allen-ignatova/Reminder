using System.Collections.Generic;
using DI.Reminder.Data.CategoryDataBase;
using DI.Reminder.Data;
using DI.Reminder.Common.CategoryModel;
using System;
namespace DI.Reminder.BL.CategoryStorage
{
    public class Categories:ICategories
    {
        private ICategoryRepository _category;
        public Categories(ICategoryRepository category)
        {
            _category = category;
            if (_category == null)
                throw new ArgumentNullException();
        }

        public void DeleteCategory(int? id)
        {
            if (id == null || id<0)
                return;
            _category.DeleteCategory((int)id);
        }

        public IList<Category> GetCategories(int? id)
        {
            if (id < 0)
                return null;
            try
            {
                return _category.GetCategories(id);
            }
            catch
            {
                throw;
            }
        }
        public int? GetCategoryParentID(string categoryName)
        {
            try
            {
                return _category.GetCategoryParentID(categoryName);
            }
            catch
            {
                throw;
            }

        }
        public void InsertCategory(Category category)
        {
            if (category == null)
                return;
            _category.AddCategory(category);
        }
    }
}
