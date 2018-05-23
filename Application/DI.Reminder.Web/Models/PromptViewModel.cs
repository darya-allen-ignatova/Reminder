using System.Collections.Generic;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Common.PromptModel;


namespace DI.Reminder.Web.Models
{
    public class PromptViewModel
    {
        public IList<Category> CategoryList { get; set; }
        public IList<Prompt> PromptList { get; set; }
    }
}