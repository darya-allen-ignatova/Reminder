using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.BL.CachedRepository
{
    public interface ICacheRepository
    {
        T GetValueOfCache<T>(int id) where T:class;
        bool AddCache<T>(T value, int id) where T : class;
        void UpdateCache<T>(T value, int id) where T : class;
        void DeleteCache(int id);
    }
}
