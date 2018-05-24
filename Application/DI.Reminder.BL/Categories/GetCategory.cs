using System.Collections.Generic;
using DI.Reminder.Data.DataBase;
using DI.Reminder.Data;
using DI.Reminder.Common.CategoryModel;
using System;
namespace DI.Reminder.BL.Categories
{
    public class GetCategory:IGetCategories
    {
        private ICategoryRepository _getCategory;
        public GetCategory(ICategoryRepository getCategory)
        {
            _getCategory = getCategory;
            if (_getCategory == null)
                throw new ArgumentNullException();
        }
        public IList<Category> GetCategories(int? id)
        {
            if (id < 0)
                return null;
            return _getCategory.GetCategories(id);
        }
        public int? GetCategoryID(string categoryName)
        {
            return _getCategory.GetCategoryID(categoryName);
        }
    }
}
