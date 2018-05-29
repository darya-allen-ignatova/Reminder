using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DI.Reminder.Common.LoginModels
{
    public class Account
    {
        public int ID { get; set; }
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords aren't similar")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordConfirm { get; set; }
        public List<Role> roles { get; set; }
    }
}
