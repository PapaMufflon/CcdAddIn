using System.Windows.Input;
using CcdAddIn.UI.CleanCodeDeveloper;
using Microsoft.Practices.Prism.Commands;

namespace CcdAddIn.UI.Views
{
    public class WhiteLevelViewModel
    {
        private readonly CcdLevel _currentLevel;

        public WhiteLevelViewModel(CcdLevel currentLevel)
        {
            _currentLevel = currentLevel;
        }

        public ICommand RestartCommand
        {
            get
            {
                return new DelegateCommand(() => _currentLevel.Advance());
            }
        }
    }
}