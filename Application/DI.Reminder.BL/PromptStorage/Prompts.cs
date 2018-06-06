using System.Collections.Generic;
using System;
using DI.Reminder.Data.PromptDataBase;
using DI.Reminder.Data;
using DI.Reminder.Common.PromptModel;
using DI.Reminder.Data.CategoryDataBase;

namespace DI.Reminder.BL.PromptStorage
{
    public class Prompts:IPrompts
    {
        private ICategoryRepository _categoryRepository;
        private IPromptRepository _promptRepository;
        public Prompts(IPromptRepository promptRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _promptRepository = promptRepository;
            if (_promptRepository == null)
                throw new ArgumentNullException();
        }
        public IList<Prompt> GetCategoryItemsByID(int userID,int? id)
        {
            if (id == null)
                return null;
            else if (id < 0)
                return null;
            IList<Prompt> list;
            try
            {
                list = _promptRepository.GetPromptsList(userID,id);
            }
            catch
            {
                throw;
            }
           
            return list;
        }
        public Prompt GetPromptDetails(int userID, int? id)
        {
            if (id == null || id<0)
                return null;
            Prompt prompt;
            try
            {
                 prompt = _promptRepository.GetPrompt(userID,id);
            }
            catch
            {
                throw;
            }
            return prompt;
        }
        public void DeletePrompt(int userID, int? id)
        {
            if (id == null)
                return;
            _promptRepository.DeletePrompt(userID,id);
        }
        public void InsertPrompt(int userID, Prompt newprompt)
        {
            if (newprompt == null)
                return;
            _promptRepository.AddPrompt(userID,newprompt);
        }

    
    }
}
