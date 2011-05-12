using System;
using CcdAddIn.UI.CleanCodeDeveloper;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

namespace CcdAddIn.UI.Test
{
    public class Navigation
    {
        public class Given_the_black_level_When_advancing_a_level : WithSubject<Navigator>
        {
            private static CcdLevel _ccdLevel;

            Establish context = () =>
            {
                _ccdLevel = new CcdLevel(Level.Black);

                Subject = new Navigator(The<IRegionManager>(), The<IEventAggregator>(), _ccdLevel);
            };

            Because of = () => _ccdLevel.Advance();

            It should_navigate_to_the_colored_levels_view = () =>
            {
                var expectedView = new Uri(Navigator.CcdLevelsView, UriKind.Relative);
                The<IRegionManager>().WasToldTo(x => x.RequestNavigate("MainRegion", expectedView));
            };
        }
    }

    public static class MockedRegionManager
    {
        public static void RequestNavigate(this IRegionManager foo, string regionName, Uri source)
        {
            System.Diagnostics.Debugger.Break();
        }
    }
}
