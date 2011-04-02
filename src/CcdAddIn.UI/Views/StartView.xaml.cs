using CcdAddIn.UI.ViewModels;

namespace CcdAddIn.UI.Views
{
    public partial class StartView
    {
        public StartView(StartViewModel vm)
        {
            InitializeComponent();

            Loaded += (s, e) => DataContext = vm;
        }
    }
}
