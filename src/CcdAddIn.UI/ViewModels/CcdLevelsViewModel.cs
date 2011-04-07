using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.ViewModels
{
    public class CcdLevelsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private CcdLevel _currentLevel;
        private RetrospectiveInProgressEvent _retrospectiveInProgressEvent;

        public CcdLevelsViewModel(IEventAggregator eventAggregator)
        {
            _retrospectiveInProgressEvent = eventAggregator.GetEvent<RetrospectiveInProgressEvent>();
            _retrospectiveInProgressEvent.Subscribe(x => EvaluationVisible = x);

            _currentLevel = new CcdLevel(Level.Red);
        }

        public Level CurrentLevel
        {
            get { return _currentLevel.Level; }
        }

        public List<Item> Principles
        {
            get { return _currentLevel.Principles; }
        }

        public List<Item> Practices
        {
            get { return _currentLevel.Practices; }
        }

        private bool _evaluationVisible;

        public bool EvaluationVisible
        {
            get { return _evaluationVisible; }
            private set
            {
                _evaluationVisible = value;
                OnPropertyChanged("EvaluationVisible");
            }
        }

        public ICommand RetrospectiveDoneCommand
        {
            get
            {
                return new DelegateCommand(() => _retrospectiveInProgressEvent.Publish(false));
            }
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
