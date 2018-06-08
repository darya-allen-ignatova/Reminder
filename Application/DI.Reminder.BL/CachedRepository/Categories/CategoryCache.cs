using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using DI.Reminder.Common.CategoryModel;

namespace DI.Reminder.BL.CachedRepository.Categories
{
    public class CategoryCache : ICategoryCache
    {
      
        public bool AddCache(Category value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(value.ID.ToString(), value, DateTime.Now.AddMinutes(15));
        }

        public void DeleteCache(int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(id.ToString()))
            {
                memoryCache.Remove(id.ToString());
            }
        }

        public Category GetValueOfCache(int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(id.ToString()) as Category;
        }

        public void UpdateCache(Category value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set(value.ID.ToString(), value, DateTime.Now.AddMinutes(15));
        }
    }
}
