using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Common.PromptModel;
using System.Collections.Generic;

namespace DI.Reminder.Web.Models
{
    public class ModelCategoriesWithPrompts
    {
        public IList<Prompt> PromptList { get; set; }
        public IList<Category> CategoryList { get; set; }
        public int? previousListID { get; set; }
    }
}