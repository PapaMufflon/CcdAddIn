using System.Windows.Input;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.Views
{
    public class WhiteLevelViewModel
    {
        private GoToLevelEvent _goToLevelEvent;

        public WhiteLevelViewModel(IEventAggregator eventAggregator)
        {
            _goToLevelEvent = eventAggregator.GetEvent<GoToLevelEvent>();
        }

        public ICommand RestartCommand
        {
            get
            {
                return new DelegateCommand(() => _goToLevelEvent.Publish(Level.Red));
            }
        }
    }
}