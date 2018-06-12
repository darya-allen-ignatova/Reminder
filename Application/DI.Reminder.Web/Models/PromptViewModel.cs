using System.Collections.Generic;
using System.Web.Mvc;
using DI.Reminder.Common.PromptModel;


namespace DI.Reminder.Web.Models
{
    public class PromptViewModel
    {
        public List<SelectListItem> CategoryList { get; set; }
        public Prompt Prompt { get; set; }
    }
}