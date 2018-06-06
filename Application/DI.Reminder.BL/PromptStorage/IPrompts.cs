using System.Collections.Generic;
using DI.Reminder.Common.PromptModel;
using DI.Reminder.Data.CategoryDataBase;

namespace DI.Reminder.BL.PromptStorage
{
    public interface IPrompts
    {
        IList<Prompt> GetCategoryItemsByID(int userID,int? id);
        Prompt GetPromptDetails(int userID,int? id);
        void DeletePrompt(int userID,int? id);
        void InsertPrompt(int userID, Prompt newprompt);
    }
}
