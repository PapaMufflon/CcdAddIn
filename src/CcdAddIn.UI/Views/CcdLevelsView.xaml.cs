using CcdAddIn.UI.ViewModels;
using NLog;

namespace CcdAddIn.UI.Views
{
    public partial class CcdLevelsView
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public CcdLevelsView(CcdLevelsViewModel vm)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                _logger.Trace("Wiring CcdLevelsViewModel to CcdLevelsView");
                DataContext = vm;
            };
        }
    }
}