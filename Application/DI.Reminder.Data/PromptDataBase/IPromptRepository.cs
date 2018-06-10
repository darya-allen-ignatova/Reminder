using System.Collections.Generic;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Common.PromptModel;
using DI.Reminder.Data.CategoryDataBase;

namespace DI.Reminder.Data.PromptDataBase
{
    public interface IPromptRepository
    {
        IList<Prompt> GetPromptsList(int userID, int? id);
        Prompt GetPrompt(int userID,int? id);
        void DeletePrompt(int userID, int? id);
        void AddPrompt(int userID,Prompt prompt);
        void EditPrompt(Prompt prompt);
    }
}
