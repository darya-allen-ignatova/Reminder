using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.Common.CategoryModel
{
    public class CategoriesModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ParentCategory { get; set; }
    }
}
