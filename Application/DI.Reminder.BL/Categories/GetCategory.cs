using System.Collections.Generic;
using DI.Reminder.Data.DataBase;
using DI.Reminder.Data;
using DI.Reminder.Common.CategoryModel;

namespace DI.Reminder.BL.Categories
{
    public class GetCategory:IGetCategories
    {
        private ICategoryRepository _getCategory;
        public GetCategory(ICategoryRepository getCategory)
        {
            _getCategory = getCategory;
        }
        public IList<Category> GetCategories(int? id)
        {

            return _getCategory.GetCategories(id);
        }
        public int? GetCategoryID(string categoryName)
        {
            return _getCategory.GetCategoryID(categoryName);
        }
    }
}
