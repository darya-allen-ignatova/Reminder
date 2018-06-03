using System.Collections.Generic;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Common.PromptModel;
using DI.Reminder.Data.CategoryDataBase;

namespace DI.Reminder.Data.PromptDataBase
{
    public interface IPromptRepository
    {
        IList<Prompt> GetPromptsList(int? id);
        Prompt GetPrompt(int? id);
        void DeletePrompt(int? id);
        void AddPrompt(Prompt prompt, ICategoryRepository categoryRepository);
    }
}
