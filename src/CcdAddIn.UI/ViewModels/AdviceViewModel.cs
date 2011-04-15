using System.Windows.Input;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using CcdAddIn.UI.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.ViewModels
{
    public class AdviceViewModel
    {
        private RetrospectiveInProgressEvent _retrospectiveInProgressEvent;
        private string _advice;
        private bool _canAdvance;

        public AdviceViewModel(IEventAggregator eventAggregator, IRepository repository)
        {
            _retrospectiveInProgressEvent = eventAggregator.GetEvent<RetrospectiveInProgressEvent>();

            var shouldAdvance = RalfWestphal.ShouldAdvance(repository.Retrospectives);

            _advice = shouldAdvance ? Resources.Resource.PositiveAdvice : Resources.Resource.NegativeAdvice;
            _canAdvance = shouldAdvance;
        }

        public ICommand TakeAdviceCommand
        {
            get
            {
                return new DelegateCommand(() => _retrospectiveInProgressEvent.Publish(false));
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