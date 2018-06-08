using DI.Reminder.Common.PromptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.BL.CachedRepository.Prompts
{
    public interface IPromptCache
    {
        Prompt GetValueOfCache(int id);
        bool AddCache(Prompt value);
        void UpdateCache(Prompt value);
        void DeleteCache(int id);
    }
}
