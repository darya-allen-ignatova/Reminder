using System.Collections.Generic;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Common.PromptModel;

namespace DI.Reminder.Data.DataBase
{
    public interface IPromptRepository
    {
        IList<Prompt> GetPromptsList(int? id);
        Prompt GetPrompt(int? id);
        void DeletePrompt(int? id);
        void AddPrompt(Prompt prompt);
    }
}
