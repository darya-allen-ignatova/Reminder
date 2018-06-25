using System.ComponentModel.DataAnnotations;

namespace DI.Reminder.Common.PromptModel
{
    public class Action
    {
        [Key]
        public int ID { get; set; }
        
        public string Name { get; set; }
    }
}