using System;
using System.Windows;
using CcdAddIn.UI.Communication;
using CcdAddIn.UI.ViewModels;
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

        protected override DependencyObject CreateShell()
        {
            Container.RegisterType<object, CcdLevelsView>("CcdLevelsView");

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("HeaderRegion", typeof(HeaderView));
            regionManager.RegisterViewWithRegion("MainRegion", typeof(StartView));

            var eventAggregator = Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<ChangeLevelEvent>().Subscribe(NavigateToCcdLevelsView);
            
            Shell = new Shell();
            return Shell;
        }

        private void NavigateToCcdLevelsView(CcdLevel ccdLevel)
        {
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate("MainRegion", new Uri("CcdLevelsView", UriKind.Relative));
        }
    }
}
