using CcdAddIn.UI.ViewModels;
using NLog;

namespace CcdAddIn.UI.Views
{
    public partial class HeaderView
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public HeaderView(HeaderViewModel vm)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                _logger.Trace("Wiring HeaderViewModel to HeaderView");
                DataContext = vm;
            };
        }
    }
}
