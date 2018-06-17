using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Common.PromptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI.Reminder.Web.Models
{
    public class SearchModel
    {
        public Prompt Prompt { get; set; }
        public IList<SelectListItem> Categories { get; set; }

    }
}