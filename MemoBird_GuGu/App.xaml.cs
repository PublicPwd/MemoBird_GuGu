using MemoBird_GuGu.Utils;
using System.Windows;

namespace MemoBird_GuGu
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            CheckUpdate.Check();
        }
    }
}
