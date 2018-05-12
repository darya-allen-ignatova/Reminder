using DI.Reminder.BL.Service;
using StructureMap;

namespace DI.Reminder.BL
{
    public class BLRegistry:Registry
    {
        public BLRegistry()
        {
            For<IPrompt>().Use<GetPrompts>();
            For<IBLService>().Use<BLService>();
        }
    }
}
