using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.BL.Repository
{
    public interface IPromptRepository
    {
        IEnumerable<Prompt> GetCategoryItemsByID(int id);
        Prompt GetPromptDetails(int id);
    }
}
