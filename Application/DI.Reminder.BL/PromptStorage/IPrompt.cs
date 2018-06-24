using System.Collections.Generic;
using DI.Reminder.Common.PromptModel;

namespace DI.Reminder.BL.PromptStorage
{
    public interface IPrompt
    {
        IList<Prompt> GetCategoryItemsByID(int userID, int? id);
        Prompt GetPromptDetails(int userID, int id);
        void DeletePrompt(int userID, int id);
        void InsertPrompt(int userID, Prompt newprompt);
        IList<Prompt> GetSearchingPrompts(int UserID, string promptval, string categoryval, string dateval);
        void EditPrompt(Prompt prompt);
    }
}
