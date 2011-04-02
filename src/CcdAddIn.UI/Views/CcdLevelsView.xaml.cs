using CcdAddIn.UI.ViewModels;

namespace CcdAddIn.UI.Views
{
    public partial class CcdLevelsView
    {
        public CcdLevelsView(CcdLevelsViewModel vm)
        {
            InitializeComponent();

            Loaded += (s, e) => DataContext = vm;
        }
    }
}