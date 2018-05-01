using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.BL
{
    public interface IPrompt
    {
        IEnumerable<Prompt> GetList(string _category);
        Prompt GetPrompt(int? _id);
    }
}
