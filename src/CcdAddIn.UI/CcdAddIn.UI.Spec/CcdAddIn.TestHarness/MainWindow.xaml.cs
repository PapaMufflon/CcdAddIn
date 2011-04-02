using CcdAddIn.UI;

namespace CcdAddIn.TestHarness
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            Content = bootstrapper.Shell;

            InitializeComponent();
        }
    }
}
