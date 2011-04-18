﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using NLog;

namespace CcdAddIn.UI.ViewModels
{
    public class CcdLevelsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private CcdLevel _currentLevel;
        private ShowAdviceEvent _showAdviceEvent;

        public CcdLevelsViewModel(IEventAggregator eventAggregator)
        {
            _logger.Trace("Wiring events");
            _showAdviceEvent = eventAggregator.GetEvent<ShowAdviceEvent>();

            eventAggregator.GetEvent<BeginRetrospectiveEvent>().Subscribe(x =>
            {
                _logger.Trace("Let's begin the retrospective - switch to evaluation-mode");
                EvaluationVisible = true;
            });

            eventAggregator.GetEvent<EndRetrospectiveEvent>().Subscribe(x =>
            {
                _logger.Trace("Retrospective has ended with wish to advance? {0}", x);
                if (x)
                {
                    _currentLevel.Advance();
                    OnPropertyChanged("Principles");
                    OnPropertyChanged("Practices");
                    _logger.Trace("We advanced to level {0} and updated the principles & practices", _currentLevel.Level);
                }
            });

            _logger.Trace("Let's start with the red level");
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
                    _logger.Trace("We're done with the retrospective. Let's show the advice.");
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
