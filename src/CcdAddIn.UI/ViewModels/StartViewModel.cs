using System.Windows.Input;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using NLog;

namespace CcdAddIn.UI.ViewModels
{
    public class StartViewModel
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly GoToLevelEvent _goToLevelEvent;

        public StartViewModel(IEventAggregator eventAggregator)
        {
            _logger.Trace("Wiring events");
            _goToLevelEvent = eventAggregator.GetEvent<GoToLevelEvent>();
        }

        public ICommand GoToRedLevelCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _logger.Trace("Change level to red");
                    _goToLevelEvent.Publish(Level.Red);
                });
            }
        }
    }
}