using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
namespace DI.Reminder.Common.PromptModel
{
    public class Prompt
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatingDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan TimeOfPrompt { get; set; }
        public List<Action> Actions { get; set; }
        public string Image { get; set; }
    }
}
