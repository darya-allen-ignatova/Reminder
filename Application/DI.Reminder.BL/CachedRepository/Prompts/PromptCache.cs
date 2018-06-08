using DI.Reminder.Common.PromptModel;
using System;
using System.Runtime.Caching;

namespace DI.Reminder.BL.CachedRepository.Prompts
{
    public class PromptCache:IPromptCache
    {
        public Prompt GetValueOfCache(int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(id.ToString()) as Prompt;
        }

        public bool AddCache(Prompt value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(value.ID.ToString(), value, DateTime.Now.AddMinutes(15));
        }

        public void UpdateCache(Prompt value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set(value.ID.ToString(), value, DateTime.Now.AddMinutes(15));
        }

        public void DeleteCache(int id)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(id.ToString()))
            {
                memoryCache.Remove(id.ToString());
            }
        }
    }
}
