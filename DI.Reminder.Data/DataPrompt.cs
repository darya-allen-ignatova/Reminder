using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.Data
{
    public class DataPrompt
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatingDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public TimeSpan TimeOfPrompt { get; set; }
        public byte[] Image { get; set; }
    }
}
