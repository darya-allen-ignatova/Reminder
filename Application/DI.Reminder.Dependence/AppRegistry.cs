using StructureMap;
using DI.Reminder.Data;


namespace DI.Reminder.Dependence
{
    public class AppRegistry : Registry
    {
        public AppRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.LookForRegistries();
            }
            );
        }
    }
}
