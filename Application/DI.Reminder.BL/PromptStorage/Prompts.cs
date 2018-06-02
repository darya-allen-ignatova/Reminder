using System.Collections.Generic;
using System;
using DI.Reminder.Data.DataBase;
using DI.Reminder.Common.PromptModel;
using DI.Reminder.BL.CachedRepository;

namespace DI.Reminder.BL.PromptStorage
{
    public class Prompts:IPrompt
    {
        private IPromptRepository _prompts;
        ICacheRepository _cacheRepository;
        public Prompts(IPromptRepository prompts, ICacheRepository cacheRepository)
        {
            _prompts = prompts;
            _cacheRepository = cacheRepository;
            if (_prompts == null)
                throw new ArgumentNullException();
        }
        public IList<Prompt> GetCategoryItemsByID(int? id)
        {
            if (id == null)
                return null;
            else if (id < 0)
                return null;
            IList<Prompt> list;
            try
            {
                list = _prompts.GetPromptsList(id);
            }
            catch
            {
                throw;
            }
           
            return list;
        }
        public Prompt GetPromptDetails(int? id)
        {
            if (id == null || id<0)
                return null;
            Prompt prompt = _cacheRepository.GetValueOfCache((int)id);
            if (prompt == null)
            {
                prompt = _prompts.GetPrompt(id);
                _cacheRepository.AddCache(prompt);
            }
            return prompt;
        }
        public void DeletePrompt(int? id)
        {
            if (id == null)
                return;
            _prompts.DeletePrompt(id);
            _cacheRepository.DeleteCache((int)id);
        }
        public void InsertPrompt(Prompt newprompt, ICategoryRepository categoryRepository)
        {
            if (newprompt == null)
                return;
            _prompts.AddPrompt(newprompt, categoryRepository);
            _cacheRepository.AddCache(newprompt);
        }

    
    }
}
