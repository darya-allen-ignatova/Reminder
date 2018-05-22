using System.Collections.Generic;

namespace DI.Reminder.BL
{
    public interface IPrompt
    {
        IEnumerable<Prompt> GetAll();
        IEnumerable<Prompt> GetList(string _category);
        Prompt GetPrompt(int? _id);
    }
}
