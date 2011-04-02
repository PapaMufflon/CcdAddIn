using CcdAddIn.UI.ViewModels;

namespace CcdAddIn.UI.Views
{
    public partial class CcdLevelsView
    {
        public CcdLevelsView()
        {
            DataContext = new CcdLevelsViewModel();
            InitializeComponent();
        }
    }
}
