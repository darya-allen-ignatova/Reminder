using System.Collections.Generic;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Common.PromptModel;

namespace DI.Reminder.Data.PromptDataBase
{
    public interface IPromptRepository
    {
        IList<Prompt> GetPromptsList(int? id);
        Prompt GetPrompt(int? id);
    }
}
