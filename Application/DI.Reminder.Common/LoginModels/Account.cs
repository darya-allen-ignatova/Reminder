using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.Common.LoginModels
{
    public class Account
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Role> roles { get; set; }
    }
}
