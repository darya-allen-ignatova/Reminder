using DI.Reminder.Common.LoginModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DI.Reminder.Web.Models
{
    public class ModelAccountWithRoles
    {
        public Account Account { get; set; }
        public IList<SelectListItem> AllRoles { get; set; }
    }
}