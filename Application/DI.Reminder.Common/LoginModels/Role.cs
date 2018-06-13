using System.ComponentModel.DataAnnotations;

namespace DI.Reminder.Common.LoginModels
{
    public class Role
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Field must be filled")]
        public string Name { get; set; }
    }
}
