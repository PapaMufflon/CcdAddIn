using System.Collections.Generic;
using System.Windows;
using CcdAddIn.UI.CleanCodeDeveloper;
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
            Container.RegisterType<IPersister<List<CcdLevel>>, CcdLevelFilePersister>();
            Container.RegisterType<IRepository, Repository>(new ContainerControlledLifetimeManager());

            var repository = Container.Resolve<IRepository>() as Repository;
            if (repository != null)
                Container.RegisterInstance(repository.CurrentLevel, new ContainerControlledLifetimeManager());
            else
                _logger.Fatal("Cannot resolve repository.");

            Container.RegisterInstance(Container.Resolve<Navigator>(), new ContainerControlledLifetimeManager());
            Container.RegisterInstance(Container.Resolve<PersistService>(), new ContainerControlledLifetimeManager());
            Container.RegisterInstance<IRalfWestphal>(Container.Resolve<RalfWestphal>(), new ContainerControlledLifetimeManager());
        }

        private void RegisterViews()
        {
            _logger.Trace("Registering views");
            Container.RegisterInstance<object>(Navigator.CcdLevelsView, Container.Resolve<CcdLevelsView>());
            Container.RegisterType<object, AdviceView>(Navigator.AdviceView);
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
