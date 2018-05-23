using System.Collections.Generic;
using System.Linq;
using DI.Reminder.Data.DataBase;
using DI.Reminder.Data;
using DI.Reminder.Common.PromptModel;

namespace DI.Reminder.BL.PromptStorage
{
    public class Prompts:IPrompt
    {
        private IPromptRepository _getprompts;
        public Prompts(IPromptRepository getprompts)
        {
            _getprompts = getprompts;
        }
        public IList<Prompt> GetCategoryItemsByID(int? id)
        {
            IList<Prompt> list = _getprompts.GetPromptsList(id);
            return list;
        }
        public Prompt GetPromptDetails(int? id)
        {
            if (id == null)
                return null;
            Prompt prompt = _getprompts.GetPrompt(id);
            return prompt;
        }

    
    }
}
