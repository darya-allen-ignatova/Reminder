using System.Collections.Generic;
using System;
using DI.Reminder.Data.DataBase;
using DI.Reminder.Data;
using DI.Reminder.Common.PromptModel;
using System;

namespace DI.Reminder.BL.PromptStorage
{
    public class Prompts:IPrompt
    {
        private IPromptRepository _prompts;
        public Prompts(IPromptRepository prompts)
        {
            _prompts = prompts;
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
            Prompt prompt;
            try
            {
                 prompt = _prompts.GetPrompt(id);
            }
            catch
            {
                throw;
            }
            return prompt;
        }
        public void DeletePrompt(int? id)
        {
            if (id == null)
                return;
            _prompts.DeletePrompt(id);
        }
        public void InsertPrompt(Prompt newprompt)
        {
            if (newprompt == null)
                return;
            _prompts.AddPrompt(newprompt);
        }

    
    }
}
