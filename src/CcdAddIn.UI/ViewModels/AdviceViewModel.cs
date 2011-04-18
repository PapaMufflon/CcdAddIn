using System.Windows.Input;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using CcdAddIn.UI.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using NLog;

namespace CcdAddIn.UI.ViewModels
{
    public class AdviceViewModel
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private EndRetrospectiveEvent _endRetrospectiveEvent;
        private string _advice;
        private bool _canAdvance;

        public AdviceViewModel(IEventAggregator eventAggregator, IRepository repository)
        {
            _logger.Trace("Wiring events");
            _endRetrospectiveEvent = eventAggregator.GetEvent<EndRetrospectiveEvent>();

            _logger.Trace("Querying Ralf Westphal - should advance?");
            var shouldAdvance = RalfWestphal.ShouldAdvance(repository.Retrospectives);

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
                    _endRetrospectiveEvent.Publish(_canAdvance);   
                });
            }
        }

        public ICommand DenyAdviceCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _logger.Trace("We do not take the advice. So, do we want to advance? {0}", !_canAdvance);
                    _endRetrospectiveEvent.Publish(!_canAdvance);
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
    }
}