using System;
using System.Collections.Generic;

namespace DI.Reminder.Common.PromptModel
{
    public class Prompt
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatingDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public TimeSpan TimeOfPrompt { get; set; }
        public List<Action> Actions { get; set; }
        public string Image { get; set; }
    }
}
