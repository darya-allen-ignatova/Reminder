using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DI.Reminder.BL;
using DI.Reminder.BL.Category;
using DI.Reminder.BL.Repository;


namespace DI.Reminder.Web.Models
{
    public class Ins
    {
        public IList<Category> CategoryList { get; set; }
        public IList<Prompt> PromptList { get; set; }
    }
}