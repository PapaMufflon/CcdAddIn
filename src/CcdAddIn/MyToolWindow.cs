using System.Runtime.InteropServices;
using CcdAddIn.UI;
using Microsoft.VisualStudio.Shell;

namespace OpenSource.CcdAddIn
{
    [Guid("f76daf6f-5ac3-47a8-87d4-19c3136ef40f")]
    public class MyToolWindow : ToolWindowPane
    {
        public MyToolWindow() :
            base(null)
        {
            this.Caption = Resources.ToolWindowTitle;
            this.BitmapResourceID = 301;
            this.BitmapIndex = 1;

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            base.Content = bootstrapper.Shell;
        }
    }
}