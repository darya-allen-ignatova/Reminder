using DI.Reminder.BL.Service;
using DI.Reminder.BL.Repository;
using StructureMap;

namespace DI.Reminder.BL
{
    public class BLRegistry:Registry
    {
        public BLRegistry()
        {
            For<IPrompt>().Use<GetPrompts>();
            For<IBLService>().Use<BLService>();
            For<IPromptRepository>().Use<PromptRepository>();
        }
    }
}
