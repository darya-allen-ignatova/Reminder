using StructureMap;

namespace DI.Reminder.BL
{
    class BLRegistry:Registry
    {
        public BLRegistry()
        {
            For<IPrompt>().Use<GetPrompts>();
        }
    }
}
