using System.Collections.Generic;
using System;
using DI.Reminder.Common.PromptModel;
using DI.Reminder.Data.PromptDataBase;
using DI.Reminder.Data.Searching;
using DI.Reminder.Data.CategoryDataBase;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.BL.CachedRepository;

namespace DI.Reminder.BL.PromptStorage
{
    public class Prompts:IPrompt
    {
        private ICacheRepository _cacheRepository;
        private ISearch _search;
        private IPromptRepository _promptRepository;
        private ICategoryRepository _categoryRepository;
        public Prompts(IPromptRepository promptRepository, ICategoryRepository categoryRepository, ISearch search, ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
            _categoryRepository = categoryRepository;
            _search = search;
            _promptRepository = promptRepository;
            if (_promptRepository == null)
                throw new ArgumentNullException();
        }
        public IList<Prompt> GetCategoryItemsByID(int userID,int? id)
        {
            if (id == null || id < 0 || userID < 1)
                return null;
            IList<Prompt> promptList;
            IList<Category> categoryList;
            try
            {
                promptList = _promptRepository.GetPromptsList(userID,id);
                categoryList = _categoryRepository.GetCategories(id);
            }
            catch
            {
                throw;
            }
            if (promptList == null & categoryList == null)
                return null;
            else if (promptList == null & categoryList!= null)
            {
                promptList = new List<Prompt>();
                return promptList;
            }
            else
                return promptList;
        }
        public Prompt GetPromptDetails(int userID, int? id)
        {
            if (id == null || id<0 || userID<1)
                return null;
            Prompt prompt = _cacheRepository.GetValueOfCache<Prompt>((int)id);
            if (prompt == null)
            try
            {
                 prompt = _promptRepository.GetPrompt(userID,id);
                _cacheRepository.AddCache<Prompt>(prompt, prompt.ID);
            }
            catch
            {
                
            }
            return prompt;
        }
        public void DeletePrompt(int userID, int? id)
        {
            if (id == null || userID<1 || id<1)
                return;
            _promptRepository.DeletePrompt(userID,id);
            _cacheRepository.DeleteCache((int)id);

        }

        public void InsertPrompt(int userID, Prompt newprompt)
        {
            if (newprompt == null || userID<1)
                return;
            _promptRepository.AddPrompt(userID,newprompt);
            _cacheRepository.AddCache<Prompt>(newprompt, newprompt.ID);
        }

        public IList<Prompt> GetSearchingPrompts(int userID,int id, string value)
        {
            if (value == null || id<1 || userID<1)
                return null;
            return _search.GetSearchItems(userID, id, value);
        }
    
    }
}
