using System;
using System.Windows;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using CcdAddIn.UI.Data;
using CcdAddIn.UI.Views;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using NLog;

namespace CcdAddIn.UI
{
    public class Bootstrapper : UnityBootstrapper
    {
        public Shell Shell { get; private set; }

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private const string AdviceView = "AdviceView";
        private const string CcdLevelsView = "CcdLevelsView";
        private const string WhiteLevelView = "WhiteLevelView";

        protected override DependencyObject CreateShell()
        {
            _logger.Trace("Registering types");
            Container.RegisterType<IRepository, Repository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFileService, FileService>();

            _logger.Trace("Registering views");
            Container.RegisterType<object, CcdLevelsView>(CcdLevelsView);
            Container.RegisterType<object, AdviceView>(AdviceView);
            Container.RegisterType<object, WhiteLevelView>(WhiteLevelView);

            _logger.Trace("Mapping regions");
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("HeaderRegion", typeof(HeaderView));
            regionManager.RegisterViewWithRegion("MainRegion", typeof(StartView));

            _logger.Trace("Wiring events");
            var eventAggregator = Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<GoToLevelEvent>().Subscribe(NavigateToCcdLevelsView);
            eventAggregator.GetEvent<ShowAdviceEvent>().Subscribe(NavigateToShowAdviceView);

            _logger.Trace("Creating Shell");
            Shell = new Shell();
            return Shell;
        }

        private void NavigateToCcdLevelsView(Level ccdLevel)
        {
            _logger.Trace("Navigate to CcdLevelsView of level {0}", ccdLevel);
            var regionManager = Container.Resolve<IRegionManager>();
            var view = ccdLevel != Level.White ?
                       new Uri(CcdLevelsView, UriKind.Relative) :
                       new Uri(WhiteLevelView, UriKind.Relative);

            regionManager.RequestNavigate("MainRegion", view);
        }

        private void NavigateBackToCcdLevelsView(bool advanceToNextLevel)
        {
            _logger.Trace("Navigate to CcdLevelsView");
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate("MainRegion", new Uri(CcdLevelsView, UriKind.Relative));
        }

        private void NavigateToShowAdviceView(object obj)
        {
            _logger.Trace("Navigate to AdviceView");
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate("MainRegion", new Uri(AdviceView, UriKind.Relative));
        }
    }
}
