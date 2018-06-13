using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DI.Reminder.Common.PromptModel
{
    public class Prompt
    {
        [Key]
        public int ID { get; set; }
        public int userID { get; set; }
        [Required(ErrorMessage = "Field must be filled")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Field must be filled")]
        public DateTime CreatingDate { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Field must be filled")]
        public int Category { get; set; }
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Field must be filled")]
        public TimeSpan TimeOfPrompt { get; set; }
        public List<Action> Actions { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
    }
}
