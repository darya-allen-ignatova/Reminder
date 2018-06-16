using System.Collections.Generic;
using DI.Reminder.Data.CategoryDataBase;
using DI.Reminder.Common.CategoryModel;
using System;
using DI.Reminder.BL.Cache;

namespace DI.Reminder.BL.CategoryStorage
{
    public class Categories : ICategories
    {
        private ICategoryRepository _category;
        private ICacheService _cacheService;
        public Categories(ICategoryRepository category, ICacheService cacheService)
        {
            _category = category ?? throw new ArgumentNullException(nameof(category));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public void DeleteCategory(int id)
        {
            if (id < 1)
                return;
            _category.DeleteCategory(id);
            _cacheService.DeleteCache(id);
        }

        public IList<Category> GetAllCategories()
        {
            return _category.GetAllCategories();
        }

        public IList<Category> GetCategories(int? id)
        {
            if (id < 0)
                return null;
            return _category.GetCategories(id);
        }

        public Category GetCategory(int id)
        {
            if (id < 1)
                return null;
            var category = _cacheService.GetValueOfCache<Category>(id);
            if (category == null)
            {
                category = _category.GetCategory(id);
                _cacheService.AddCache(category, category.ID);
            }
            return category;
        }

        public int? GetCategoryParentID(string categoryName)
        {
            if (categoryName == null)
                return null;
            return _category.GetCategoryParentID(categoryName);
        }


        public void InsertCategory(Category category)
        {
            if (category == null)
                return;
            _category.AddCategory(category);
            _cacheService.AddCache(category, category.ID);
        }
        public int? GetCategoryIDByName(string Name)
        {
            if (Name == null)
                return null;
            return _category.GetCategoryID(Name);
        }
        public void EditCategory(Category category)
        {
            _category.EditCategory(category);
            _cacheService.UpdateCache(category, category.ID);
        }
    }
}
