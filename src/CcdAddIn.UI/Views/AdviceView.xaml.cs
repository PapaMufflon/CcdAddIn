using CcdAddIn.UI.ViewModels;

namespace CcdAddIn.UI.Views
{
    public partial class AdviceView
    {
        public AdviceView(AdviceViewModel vm)
        {
            InitializeComponent();

            Loaded += (s, e) => DataContext = vm;
        }
    }
}
