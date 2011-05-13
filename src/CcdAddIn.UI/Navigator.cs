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
        public const string WhiteLevelView = "WhiteLevelView";

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IRegionManager _regionManager;
        private readonly CcdLevel _currentLevel;

        public Navigator(IRegionManager regionManager, IEventAggregator eventAggregator, CcdLevel currentLevel)
        {
            _regionManager = regionManager;
            _currentLevel = currentLevel;

            eventAggregator.GetEvent<ShowAdviceEvent>().Subscribe(NavigateToShowAdviceView);
            eventAggregator.GetEvent<EndRetrospectiveEvent>().Subscribe(o => NavigateToCcdLevelsView(null, null));
            currentLevel.Advanced += NavigateToCcdLevelsView;
        }

        private void NavigateToShowAdviceView(object obj)
        {
            _logger.Trace("Navigate to AdviceView");
            _regionManager.RequestNavigate("MainRegion", new Uri(AdviceView, UriKind.Relative));
        }

        private void NavigateToCcdLevelsView(object sender, EventArgs e)
        {
            _logger.Trace("Navigate to CcdLevelsView of level {0}", _currentLevel);
            var view = _currentLevel.Level != Level.White ?
                       new Uri(CcdLevelsView, UriKind.Relative) :
                       new Uri(WhiteLevelView, UriKind.Relative);

            _regionManager.RequestNavigate("MainRegion", view);
        }
    }
}
