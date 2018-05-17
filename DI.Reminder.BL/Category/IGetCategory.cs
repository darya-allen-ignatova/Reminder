using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.BL.Category
{
    public interface IGetCategory
    {
        IList<Category> Get(int? id);
    }
}
