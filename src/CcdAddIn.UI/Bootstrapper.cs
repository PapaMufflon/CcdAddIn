﻿using System.Windows;
using CcdAddIn.UI.Data;
using CcdAddIn.UI.Views;
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

        protected override DependencyObject CreateShell()
        {
            RegisterTypes();
            RegisterViews();
            MapRegions();

            _logger.Trace("Creating Shell");
            Shell = new Shell();
            return Shell;
        }

        private void RegisterTypes()
        {
            _logger.Trace("Registering types");

            Container.RegisterType<IFileService, FileService>();
            Container.RegisterType<IRepository, Repository>(new ContainerControlledLifetimeManager());
            var repository = Container.Resolve<IRepository>();
            Container.RegisterInstance(repository.CurrentLevel, new ContainerControlledLifetimeManager());

            Container.RegisterInstance(Container.Resolve<Navigator>(), new ContainerControlledLifetimeManager());
            Container.RegisterInstance(Container.Resolve<PersistService>(), new ContainerControlledLifetimeManager());
        }

        private void RegisterViews()
        {
            _logger.Trace("Registering views");
            Container.RegisterInstance<object>(Navigator.CcdLevelsView, Container.Resolve<CcdLevelsView>());
            Container.RegisterInstance<object>(Navigator.AdviceView, Container.Resolve<AdviceView>());
            Container.RegisterInstance<object>(Navigator.BlackLevelView, Container.Resolve<BlackLevelView>());
            Container.RegisterInstance<object>(Navigator.WhiteLevelView, Container.Resolve<WhiteLevelView>());
        }

        private void MapRegions()
        {
            _logger.Trace("Mapping regions");
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("HeaderRegion", typeof(HeaderView));
            regionManager.RegisterViewWithRegion("MainRegion", () =>
                Container.Resolve<object>(Container.Resolve<Navigator>().GetCurrentCcdLevelsUri().OriginalString));
        }
    }
}
