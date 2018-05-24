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
        private IPromptRepository _getprompts;
        public Prompts(IPromptRepository getprompts)
        {
            _getprompts = getprompts;
            if (_getprompts == null)
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
                list = _getprompts.GetPromptsList(id);
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
                 prompt = _getprompts.GetPrompt(id);
            }
            catch
            {
                throw;
            }
            return prompt;
        }

    
    }
}
