using CcdAddIn.UI.ViewModels;

namespace CcdAddIn.UI.Views
{
    public partial class HeaderView
    {
        public HeaderView(HeaderViewModel vm)
        {
            InitializeComponent();

            Loaded += (s, e) => DataContext = vm;
        }
    }
}
