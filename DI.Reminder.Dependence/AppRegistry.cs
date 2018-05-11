using StructureMap;
using DI.Reminder.BL;


namespace DI.Reminder.Dependence
{
    public class AppRegistry:Registry 
    {
        public AppRegistry()
        {
           Scan(scan => {
               scan.AssembliesFromApplicationBaseDirectory();
               //scan.Assembly(typeof(BLRegistry).Assembly);
                          scan.LookForRegistries();
                        }
           );
        }
    }
}
