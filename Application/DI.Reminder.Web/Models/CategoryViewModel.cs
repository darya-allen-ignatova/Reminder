using DI.Reminder.Common.CategoryModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DI.Reminder.Web.Models
{
    public class CategoryViewModel
    {
        public Category category { get; set; }
        public List<SelectListItem> AllCategories { get; set; }
    }
}