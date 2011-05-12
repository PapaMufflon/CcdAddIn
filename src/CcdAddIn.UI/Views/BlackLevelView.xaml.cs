using CcdAddIn.UI.ViewModels;
using NLog;

namespace CcdAddIn.UI.Views
{
    public partial class BlackLevelView
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public BlackLevelView(BlackLevelViewModel vm)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                _logger.Trace("Wiring BlackLevelViewModel to BlackLevelView");
                DataContext = vm;
            };
        }
    }
}
