using System.ComponentModel;
using System.Windows.Input;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.ViewModels
{
    public class StartViewModel
    {
        private readonly ChangeLevelEvent _changeLevelEvent;

        public StartViewModel(IEventAggregator eventAggregator)
        {
            _changeLevelEvent = eventAggregator.GetEvent<ChangeLevelEvent>();
        }

        public ICommand GoToRedLevelCommand
        {
            get
            {
                return new DelegateCommand(() => _changeLevelEvent.Publish(Level.Red));
            }
        }
    }
}