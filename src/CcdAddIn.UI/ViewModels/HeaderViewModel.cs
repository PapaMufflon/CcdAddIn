using System.ComponentModel;
using System.Windows.Input;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using NLog;

namespace CcdAddIn.UI.ViewModels
{
    public class HeaderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private BeginRetrospectiveEvent _beginRetrospectiveEvent;

        public HeaderViewModel(IEventAggregator eventAggregator, CcdLevel currentLevel)
        {
            _logger.Trace("Wiring events");
            _beginRetrospectiveEvent = eventAggregator.GetEvent<BeginRetrospectiveEvent>();

            eventAggregator.GetEvent<EndRetrospectiveEvent>().Subscribe(_ => DeceideIfRetrospectiveIsAvailable(currentLevel));
            currentLevel.Advanced += (s, e) => DeceideIfRetrospectiveIsAvailable(currentLevel);

            DeceideIfRetrospectiveIsAvailable(currentLevel);
        }

        private void DeceideIfRetrospectiveIsAvailable(CcdLevel currentLevel)
        {
            if (currentLevel.Level != Level.Black && currentLevel.Level != Level.White)
                RetrospectiveAvailable = true;
        }

        public ICommand BeginRetrospectiveCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _logger.Trace("Let's begin a retrospective");
                    RetrospectiveAvailable = false;
                    _beginRetrospectiveEvent.Publish(true);
                });
            }
        }

        private bool _retrospectiveAvailable;
        public bool RetrospectiveAvailable
        {
            get { return _retrospectiveAvailable; }
            private set
            {
                _retrospectiveAvailable = value;
                OnPropertyChanged("RetrospectiveAvailable");
            }
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
