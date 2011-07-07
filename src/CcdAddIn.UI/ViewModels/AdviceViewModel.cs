using System.Windows.Input;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using CcdAddIn.UI.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using NLog;

namespace CcdAddIn.UI.ViewModels
{
    public class AdviceViewModel : INavigationAware
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IRepository _repository;
        private readonly IRalfWestphal _ralfWestphal;
        private readonly CcdLevel _currentLevel;
        private AdviceGivenEvent _adviceGivenEvent;
        private string _advice;
        private bool _canAdvance;

        public AdviceViewModel(IEventAggregator eventAggregator,
                               IRepository repository,
                               IRalfWestphal ralfWestphal,
                               CcdLevel currentLevel)
        {
            _repository = repository;
            _ralfWestphal = ralfWestphal;
            _currentLevel = currentLevel;

            _logger.Trace("Wiring events");
            _adviceGivenEvent = eventAggregator.GetEvent<AdviceGivenEvent>();

            QueryRalfWestphal();
        }

        private void QueryRalfWestphal()
        {
            _logger.Trace("Querying Ralf Westphal - should advance?");
            var shouldAdvance = _ralfWestphal.ShouldAdvance(_repository.Retrospectives);

            _advice = shouldAdvance ? Resources.Resource.PositiveAdvice : Resources.Resource.NegativeAdvice;
            _canAdvance = shouldAdvance;
            _logger.Trace("Ralf Westphal queried. This was his advice: {0}. So - can we advance? {1}", _advice, _canAdvance);
        }

        public ICommand TakeAdviceCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _logger.Trace("We take the advice. So, do we want to advance? {0}", _canAdvance);
                    EndAdvice(CanAdvance);
                });
            }
        }

        private void EndAdvice(bool canAdvance)
        {
            if (canAdvance)
                _currentLevel.Advance();

            _adviceGivenEvent.Publish(null);
        }

        public ICommand DenyAdviceCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _logger.Trace("We do not take the advice. So, do we want to advance? {0}", !_canAdvance);
                    EndAdvice(!CanAdvance);
                });
            }
        }

        public string Advice
        {
            get { return _advice; }
            private set
            {
                _advice = value;
            }
        }

        public bool CanAdvance
        {
            get { return _canAdvance; }
            private set
            {
                _canAdvance = value;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            QueryRalfWestphal();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return navigationContext.Uri.OriginalString == Navigator.AdviceView;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            // nothing to do
        }
    }
}