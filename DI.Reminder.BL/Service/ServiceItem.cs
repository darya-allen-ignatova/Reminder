using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.BL
{
    public class ServiceItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public byte[] Image { get; set; }

    }
}
