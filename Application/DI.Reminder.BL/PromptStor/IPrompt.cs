using System.Collections.Generic;
using DI.Reminder.Common.PromptModel;

namespace DI.Reminder.BL.PromptStor
{
    public interface IPrompt
    {
        IEnumerable<Prompt> GetAllPrompts();
        IEnumerable<Prompt> GetPromptsByCategory(string _category);
        Prompt GetPrompt(int? _id);
    }
}
