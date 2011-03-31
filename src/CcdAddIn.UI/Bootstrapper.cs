using System.Windows;
using CcdAddIn.UI.Views;
using Microsoft.Practices.Prism.UnityExtensions;

namespace CcdAddIn.UI
{
    public class Bootstrapper : UnityBootstrapper
    {
        public Shell Shell { get;  private set; }

        protected override DependencyObject CreateShell()
        {
            Shell = new Shell();
            return Shell;
        }
    }
}
