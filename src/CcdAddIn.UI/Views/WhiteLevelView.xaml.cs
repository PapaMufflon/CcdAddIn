namespace CcdAddIn.UI.Views
{
    public partial class WhiteLevelView
    {
        public WhiteLevelView(WhiteLevelViewModel whiteLevelViewModel)
        {
            InitializeComponent();

            Loaded += (s, e) => DataContext = whiteLevelViewModel;
        }
    }
}
