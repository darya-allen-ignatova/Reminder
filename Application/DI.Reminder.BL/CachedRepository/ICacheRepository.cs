using DI.Reminder.Common.PromptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.BL.CachedRepository
{
    public interface ICacheRepository
    {
        Prompt GetValueOfCache(int id);
        bool AddCache(Prompt value);
        void UpdateCache(Prompt value);
        void DeleteCache(int id);
    }
}
