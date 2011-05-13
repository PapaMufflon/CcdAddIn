using System;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

namespace CcdAddIn.UI.Test
{
    //public class Given_the_black_level_When_advancing_a_level : WithSubject<Navigator>
    //{
    //    private static CcdLevel _ccdLevel;

    //    Establish context = () =>
    //    {
    //        _ccdLevel = new CcdLevel(Level.Black);

    //        The<IEventAggregator>()
    //            .WhenToldTo(x => x.GetEvent<ShowAdviceEvent>())
    //            .Return(new ShowAdviceEvent());

    //        The<IEventAggregator>()
    //            .WhenToldTo(x => x.GetEvent<EndRetrospectiveEvent>())
    //            .Return(new EndRetrospectiveEvent());

    //        Subject = new Navigator(The<IRegionManager>(), The<IEventAggregator>(), _ccdLevel);
    //    };

    //    Because of = () => _ccdLevel.Advance();

    //    It should_navigate_to_the_colored_levels_view = () =>
    //    {
    //        var expectedView = new Uri(Navigator.CcdLevelsView, UriKind.Relative);

    //        // was not called because RequestNavigate is an extension-method of
    //        // the IRegionManager-interface. Can't be mocked with RhinoMocks :(
    //        The<IRegionManager>().WasToldTo(x => x.RequestNavigate("MainRegion", expectedView));
    //    };
    //}
}