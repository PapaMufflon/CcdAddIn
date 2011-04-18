using CcdAddIn.UI.ViewModels;
using NLog;

namespace CcdAddIn.UI.Views
{
    public partial class StartView
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public StartView(StartViewModel vm)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                _logger.Trace("Wiring StartViewModel to StartView");
                DataContext = vm;
            };
        }
    }
}
