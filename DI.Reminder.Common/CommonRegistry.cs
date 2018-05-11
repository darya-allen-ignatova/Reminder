using StructureMap;

namespace DI.Reminder.Common.Logger
{
    public class CommonRegistry:Registry
    {
        public CommonRegistry()
        {
            For<ILogger>().Use<AppLogger>();
        }
    }
}
