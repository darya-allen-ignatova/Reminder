using DI.Reminder.Common.CategoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.BL.CachedRepository.Categories
{
    public interface ICategoryCache
    {
        Category GetValueOfCache(int id);
        bool AddCache(Category value);
        void UpdateCache(Category value);
        void DeleteCache(int id);
    }
}
