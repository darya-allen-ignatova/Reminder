using System;
using System.Runtime.Caching;

namespace DI.Reminder.BL.CachedRepository
{
    public class CacheRepository:ICacheRepository
    {
        public bool AddCache<T>(T value, int id)where T:class
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(id.ToString(), value, DateTime.Now.AddMinutes(15));
        }

        public void DeleteCache(int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(id.ToString()))
            {
                memoryCache.Remove(id.ToString());
            }
        }

        public T GetValueOfCache<T>(int id) where T:class
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(id.ToString()) as T;
        }

        public void UpdateCache<T>(T value, int id) where T: class
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set(id.ToString(), value, DateTime.Now.AddMinutes(15));
        }
    }
}
