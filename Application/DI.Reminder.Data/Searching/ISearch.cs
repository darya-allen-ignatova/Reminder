﻿using DI.Reminder.Common.PromptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.Data.Searching
{
    public interface ISearch
    {
        IList<Prompt> GetSearchItems(int userID,int id, string value);
    }
}