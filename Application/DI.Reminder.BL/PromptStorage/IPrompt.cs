using System.Collections.Generic;
using DI.Reminder.Common.PromptModel;

namespace DI.Reminder.BL.PromptStorage
{
    public interface IPrompt
    {
        IList<Prompt> GetCategoryItemsByID(int? id);
        Prompt GetPromptDetails(int? id);
        
    }
}
