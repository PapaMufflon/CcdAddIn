using System;
using System.Windows;
using CcdAddIn.UI.Communication;
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

        private ChangeLevelEvent _changeLevelEvent;

        protected override DependencyObject CreateShell()
        {
            //Container.RegisterInstance("CcdLevelsView", Container.Resolve<CcdLevelsView>());

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("HeaderRegion", typeof(HeaderView));
            regionManager.RegisterViewWithRegion("MainRegion", typeof(StartView));

            var eventAggregator = Container.Resolve<IEventAggregator>();
            _changeLevelEvent = eventAggregator.GetEvent<ChangeLevelEvent>();
            _changeLevelEvent.Subscribe(x =>
                regionManager.RequestNavigate("MainRegion", new Uri("CcdLevelsView", UriKind.Relative)));
            
            Shell = new Shell();
            return Shell;
        }
    }
}
