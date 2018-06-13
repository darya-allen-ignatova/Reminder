using System.ComponentModel.DataAnnotations;

namespace DI.Reminder.Common.PromptModel
{
    public class Action
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Field must be filled")]
        public string Name { get; set; }
    }
}