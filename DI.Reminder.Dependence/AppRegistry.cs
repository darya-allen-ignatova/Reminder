using StructureMap;

namespace DI.Reminder.Dependence
{
    public class AppRegistry:Registry 
    {
        public IContainer Container;
        public AppRegistry()
        {
            Container = new Container(x => x.Scan
                  (
                      scan =>
                      {
                          scan.AssembliesFromApplicationBaseDirectory();
                          //scan.Assembly("DI.Remember.Common");
                          scan.WithDefaultConventions();
                          scan.LookForRegistries();
                      }
                  )
           );
        }
    }
}
