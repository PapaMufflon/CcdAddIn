using CcdAddIn.UI.ViewModels;
using NLog;

namespace CcdAddIn.UI.Views
{
    public partial class AdviceView
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public AdviceView(AdviceViewModel vm)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                _logger.Trace("Wiring AdviceViewModel to AdviceView");
                DataContext = vm;
            };
        }
    }
}
