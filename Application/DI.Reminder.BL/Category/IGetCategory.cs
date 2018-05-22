using System.Collections.Generic;

namespace DI.Reminder.BL.Category
{
    public interface IGetCategory
    {
        IList<Category> Get(int? id);
    }
}
