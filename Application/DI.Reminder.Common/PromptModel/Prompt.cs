using DI.Reminder.Common.CategoryModel;
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Field must be filled")]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Field must be filled")]
        public Category Category { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Field must be filled")]
        public TimeSpan TimeOfPrompt { get; set; }

        public List<Action> Actions { get; set; }

        public string Image { get; set; }
        
    }
}
