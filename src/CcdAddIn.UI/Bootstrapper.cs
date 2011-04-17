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

namespace CcdAddIn.UI
{
    public class Bootstrapper : UnityBootstrapper
    {
        public Shell Shell { get;  private set; }

        private const string AdviceView = "AdviceView";
        private const string CcdLevelsView = "CcdLevelsView";

        protected override DependencyObject CreateShell()
        {
            Container.RegisterType<IRepository, Repository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFileService, FileService>();

            Container.RegisterType<object, CcdLevelsView>(CcdLevelsView);
            Container.RegisterType<object, AdviceView>(AdviceView);

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("HeaderRegion", typeof(HeaderView));
            regionManager.RegisterViewWithRegion("MainRegion", typeof(StartView));

            var eventAggregator = Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<ChangeLevelEvent>().Subscribe(NavigateToCcdLevelsView);
            eventAggregator.GetEvent<ShowAdviceEvent>().Subscribe(NavigateToShowAdviceView);
            eventAggregator.GetEvent<EndRetrospectiveEvent>().Subscribe(NavigateBackToCcdLevelsView);
            
            Shell = new Shell();
            return Shell;
        }

        private void NavigateToCcdLevelsView(Level ccdLevel)
        {
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate("MainRegion", new Uri(CcdLevelsView, UriKind.Relative));
        }

        private void NavigateBackToCcdLevelsView(bool retrospectiveInProgress)
        {
            if (!retrospectiveInProgress)
            {
                var regionManager = Container.Resolve<IRegionManager>();
                regionManager.RequestNavigate("MainRegion", new Uri(CcdLevelsView, UriKind.Relative));
            }
        }

        private void NavigateToShowAdviceView(object obj)
        {
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate("MainRegion", new Uri(AdviceView, UriKind.Relative));
        }
    }
}
