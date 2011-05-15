using System;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using NLog;

namespace CcdAddIn.UI
{
    public class Navigator
    {
        public const string AdviceView = "AdviceView";
        public const string CcdLevelsView = "CcdLevelsView";
        public const string BlackLevelView = "BlackLevelView";
        public const string WhiteLevelView = "WhiteLevelView";

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IRegionManager _regionManager;
        private readonly CcdLevel _currentLevel;

        public Navigator(IRegionManager regionManager, IEventAggregator eventAggregator, CcdLevel currentLevel)
        {
            _regionManager = regionManager;
            _currentLevel = currentLevel;

            eventAggregator.GetEvent<ShowAdviceEvent>().Subscribe(NavigateToShowAdviceView);
            eventAggregator.GetEvent<EndRetrospectiveEvent>().Subscribe(o => NavigateToCorrectCcdLevelsView());
            currentLevel.Advanced += (s, e) => NavigateToCorrectCcdLevelsView();
        }

        private void NavigateToShowAdviceView(object obj)
        {
            _logger.Trace("Navigate to AdviceView");
            _regionManager.RequestNavigate("MainRegion", new Uri(AdviceView, UriKind.Relative));
        }

        private void NavigateToCorrectCcdLevelsView()
        {
            _logger.Trace("Navigate to CcdLevelsView of level {0}", _currentLevel);
            _regionManager.RequestNavigate("MainRegion", GetCurrentCcdLevelsUri());
        }

        public Uri GetCurrentCcdLevelsUri()
        {
            var viewName = CcdLevelsView;

            if (_currentLevel.Level == Level.Black)
                viewName = BlackLevelView;
            else if (_currentLevel.Level == Level.White)
                viewName = WhiteLevelView;
            
            _logger.Trace("Suitable URI to level {0} is {1}" + _currentLevel.Level, viewName);
            return new Uri(viewName, UriKind.Relative);
        }
    }
}