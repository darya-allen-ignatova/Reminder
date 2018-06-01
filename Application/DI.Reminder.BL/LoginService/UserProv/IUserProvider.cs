using DI.Reminder.Common.LoginModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.BL.LoginService.UserProv
{
    public interface IUserProvider
    {
        Account account { get; set; }
    }
}
