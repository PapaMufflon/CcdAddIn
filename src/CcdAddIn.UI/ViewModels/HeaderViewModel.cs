using System.ComponentModel;
using System.Windows.Input;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.ViewModels
{
    public class HeaderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private RetrospectiveInProgressEvent _retrospectiveInProgressEvent;

        public HeaderViewModel(IEventAggregator eventAggregator)
        {
            _retrospectiveInProgressEvent = eventAggregator.GetEvent<RetrospectiveInProgressEvent>();
            _retrospectiveInProgressEvent.Subscribe(x =>
            {
                if (!x)
                    RetrospectiveAvailable = true;
            });

            eventAggregator.GetEvent<ChangeLevelEvent>().Subscribe(newLevel =>
            {
                if (newLevel != Level.Black)
                    RetrospectiveAvailable = true;
            });
        }

        public ICommand BeginRetrospectiveCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    RetrospectiveAvailable = false;
                    _retrospectiveInProgressEvent.Publish(true);
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
