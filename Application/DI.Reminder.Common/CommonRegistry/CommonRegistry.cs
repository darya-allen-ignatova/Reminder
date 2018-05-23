using StructureMap;
using DI.Reminder.Common.Logger;

namespace DI.Reminder.Common.CommonRegistry
{
    public class CommonRegistry:Registry
    {
        public CommonRegistry()
        {
            For<ILogger>().Use<AppLogger>();
        }
    }
}
