using System.Collections.Generic;
using DI.Reminder.Data.CategoryDataBase;
using DI.Reminder.Common.CategoryModel;
using System;
using DI.Reminder.BL.CachedRepository;

namespace DI.Reminder.BL.CategoryStorage
{
    public class Categories:ICategories
    {
        private ICategoryRepository _category;
        private ICacheRepository _cacheRepository;
        public Categories(ICategoryRepository category, ICacheRepository cacheRepository)
        {
            _category = category ?? throw new ArgumentNullException(nameof(category));
            _cacheRepository = cacheRepository?? throw new ArgumentNullException(nameof(cacheRepository));
        }

        public void DeleteCategory(int? id)
        {
            if (id == null || id<1)
                return;
            _category.DeleteCategory((int)id);
            _cacheRepository.DeleteCache((int)id);
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

        public Category GetCategory(int? id)
        {
            if (id < 1 || id == null)
                return null;
            var category = _cacheRepository.GetValueOfCache<Category>((int)id);
            if (category == null)
            {
                category = _category.GetCategory((int)id);
                _cacheRepository.AddCache(category, category.ID);
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
            _cacheRepository.AddCache(category, category.ID);
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
        public void EditCategory(Category category)
        {
            _category.EditCategory(category);
            _cacheRepository.UpdateCache(category, category.ID);
        }
    }
}
