using System.Windows.Input;
using CcdAddIn.UI.CleanCodeDeveloper;
using Microsoft.Practices.Prism.Commands;
using NLog;

namespace CcdAddIn.UI.ViewModels
{
    public class BlackLevelViewModel
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly CcdLevel _currentLevel;

        public BlackLevelViewModel(CcdLevel currentLevel)
        {
            _currentLevel = currentLevel;
        }

        public ICommand GoToRedLevelCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _logger.Trace("Change level to red");
                    _currentLevel.Advance();
                });
            }
        }
    }
}