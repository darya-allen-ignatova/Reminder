using System.Collections.Generic;
using DI.Reminder.Data.CategoryDataBase;
using DI.Reminder.Data;
using DI.Reminder.Common.CategoryModel;
using System;
using DI.Reminder.BL.CachedRepository.Categories;

namespace DI.Reminder.BL.CategoryStorage
{
    public class Categories:ICategories
    {
        private ICategoryRepository _category;
        private ICategoryCache _categoryCache;
        public Categories(ICategoryRepository category, ICategoryCache categoryCache)
        {
            _category = category;
            _categoryCache = categoryCache;
            if (_category == null)
                throw new ArgumentNullException();
        }

        public void DeleteCategory(int? id)
        {
            if (id == null || id<0)
                return;
            _category.DeleteCategory((int)id);
            _categoryCache.DeleteCache((int)id);
        }

        public IList<Category> GetAllCategories()
        {
            return _category.GetAllCategories();
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

        public Category GetCategory(int? id)
        {
            if (id < 0 || id == null)
                return null;
            var category = _categoryCache.GetValueOfCache((int)id);
            if (category == null)
            {
                category = _category.GetCategory((int)id);
                _categoryCache.AddCache(category);
            }
            return category;
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
            _categoryCache.AddCache(category);
        }
        public int? GetCategoryIDByName(string Name)
        {
            if (Name != null)
            {
                return _category.GetCategoryID(Name);
            }
            else
                return null; 
        }
    }
}
