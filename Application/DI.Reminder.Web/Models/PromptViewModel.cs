using System.Collections.Generic;
using System.Web.Mvc;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Common.PromptModel;


namespace DI.Reminder.Web.Models
{
    public class PromptViewModel
    {
        public string Property { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public Prompt Prompt { get; set; }
    }
}