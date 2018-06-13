using System;
using System.Runtime.Caching;

namespace DI.Reminder.BL.CachedRepository
{
    public class CacheRepository:ICacheRepository
    {
        private MemoryCache memoryCache
        {
            get
            {
                return MemoryCache.Default;
            }
        }
        public bool AddCache<T>(T value, int id) where T:class
        {
            return memoryCache.Add(id.ToString(), value, DateTime.Now.AddMinutes(15));
        }

        public void DeleteCache(int id)
        {
            if (memoryCache.Contains(id.ToString()))
            {
                memoryCache.Remove(id.ToString());
            }
        }

        public T GetValueOfCache<T>(int id) where T:class
        {
            return memoryCache.Get(id.ToString()) as T;
        }

        public void UpdateCache<T>(T value, int id) where T: class
        {
            memoryCache.Set(id.ToString(), value, DateTime.Now.AddMinutes(15));
        }
    }
}
