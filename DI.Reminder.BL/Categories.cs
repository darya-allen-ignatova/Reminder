using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.BL
{
    public class Categories:ICategories
    {
        List<string> categories = new List<string>
        {
            "All",
            "Important",
            "Personal",
            "Work",
            "Others"
        };
        public IEnumerable<string> GetCategories()
        {
            return categories;
        }


    }
}
