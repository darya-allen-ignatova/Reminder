﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.BL
{
    public interface ICategories
    {
        IEnumerable<string> GetCategories();
    }
}
