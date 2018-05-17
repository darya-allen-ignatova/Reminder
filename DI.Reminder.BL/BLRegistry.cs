using DI.Reminder.BL.Service;
using DI.Reminder.BL.Repository;
using StructureMap;
using DI.Reminder.BL.Category;

namespace DI.Reminder.BL
{
    public class BLRegistry:Registry
    {
        public BLRegistry()
        {
            For<IPrompt>().Use<GetPrompts>();
            For<IBLService>().Use<BLService>();
            For<IPromptRepository>().Use<PromptRepository>();
            For<IGetCategory>().Use<GetCategory>();
        }
    }
}
