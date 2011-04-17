using System.Collections.Generic;
using System.ComponentModel;
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
        private ShowAdviceEvent _showAdviceEvent;
        private BeginRetrospectiveEvent _beginRetrospectiveEvent;

        public CcdLevelsViewModel(IEventAggregator eventAggregator)
        {
            _showAdviceEvent = eventAggregator.GetEvent<ShowAdviceEvent>();

            _beginRetrospectiveEvent = eventAggregator.GetEvent<BeginRetrospectiveEvent>();
            _beginRetrospectiveEvent.Subscribe(x => EvaluationVisible = true);

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
                return new DelegateCommand(() =>
                {
                    _showAdviceEvent.Publish(null);
                    EvaluationVisible = false;
                });
            }
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
