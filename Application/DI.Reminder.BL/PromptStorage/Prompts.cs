using System.Collections.Generic;
using System;
using DI.Reminder.Common.PromptModel;
using DI.Reminder.Data.PromptDataBase;
using DI.Reminder.Data.SearchingDatabase;
using DI.Reminder.Data.CategoryDataBase;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.BL.Cache;

namespace DI.Reminder.BL.PromptStorage
{
    public class Prompts : IPrompt
    {
        private ICacheService _cacheRepository;
        private ISearchService _search;
        private IPromptRepository _promptRepository;
        private ICategoryRepository _categoryRepository;
        public Prompts(IPromptRepository promptRepository, ICategoryRepository categoryRepository, ISearchService search, ICacheService cacheRepository)
        {
            _cacheRepository = cacheRepository ?? throw new ArgumentNullException(nameof(cacheRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _search = search ?? throw new ArgumentNullException(nameof(search));
            _promptRepository = promptRepository ?? throw new ArgumentNullException(nameof(promptRepository));
        }



        public IList<Prompt> GetCategoryItemsByID(int userID, int? id)
        {
            if ( id < 0 || userID < 1)
                return null;
            IList<Prompt> promptList = _promptRepository.GetPromptsList(userID, id);
            IList<Category> categoryList = null;
            if (id == 0)
            {
                categoryList = null;
            }
            else
            {
                categoryList = _categoryRepository.GetCategories(id);
            }

            if (promptList == null & categoryList == null)
            {
                return null;
            }
            else if (promptList == null & categoryList != null)
            {
                promptList = new List<Prompt>();
                return promptList;
            }
            else
            {
                return promptList;
            }
        }
        public Prompt GetPromptDetails(int userID, int id)
        {
            if (id < 1 || userID < 1)
                return null;
            Prompt prompt = _cacheRepository.GetValueOfCache<Prompt>((int)id);
            if (prompt == null)
            {
                prompt = _promptRepository.GetPrompt(userID, id);
                _cacheRepository.AddCache<Prompt>(prompt, prompt.ID);
            }
            return prompt;
        }
        public void DeletePrompt(int userID, int id)
        {
            if ( userID < 1 || id < 1)
                return;
            _promptRepository.DeletePrompt(userID, id);
            _cacheRepository.DeleteCache((int)id);

        }

        public void InsertPrompt(int userID, Prompt newprompt)
        {
            if (userID < 1)
                return;
            _promptRepository.AddPrompt(userID, newprompt);
            _cacheRepository.AddCache<Prompt>(newprompt, newprompt.ID);
        }

        public IList<Prompt> GetSearchingPrompts(int userID, int id, string value)
        {
            if (id < 1 || userID < 1)
                return null;
            return _search.GetSearchItems(userID, id, value);
        }
        public void EditPrompt(Prompt prompt)
        {
            _promptRepository.EditPrompt(prompt);
            _cacheRepository.UpdateCache(prompt, prompt.ID);
        }

    }
}
