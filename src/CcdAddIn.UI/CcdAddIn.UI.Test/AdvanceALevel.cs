using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using CcdAddIn.UI.ViewModels;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.Test
{
    public class Given_a_black_level_When_advancing_to_the_next_level : WithSubject<CcdLevel>
    {
        private static bool _raised;

        Establish context = () => Subject = new CcdLevel(Level.Black);

        Because of = () =>
        {
            Subject.Advanced += (s, e) => _raised = true;
            Subject.Advance();
        };

        It should_raise_an_advanced_event = () => _raised.ShouldBeTrue();
    }

    public class Given_a_red_level_When_advancing_to_the_next_level : WithSubject<CcdLevelsViewModel>
    {
        private static CcdLevel _currentLevel = new CcdLevel(Level.Red);
        private static bool _practicesRaised;
        private static bool _principlesRaised;

        Establish context = () =>
        {
            The<IEventAggregator>()
                .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                .Return(new BeginRetrospectiveEvent());

            The<IEventAggregator>()
                .WhenToldTo(x => x.GetEvent<EndRetrospectiveEvent>())
                .Return(new EndRetrospectiveEvent());

            Subject = new CcdLevelsViewModel(The<IEventAggregator>(), _currentLevel);
            Subject.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Principles")
                    _principlesRaised = true;

                if (e.PropertyName == "Practices")
                    _practicesRaised = true;
            };
        };

        Because of = () => _currentLevel.Advance();

        It should_raise_property_changed_events_for_the_practices_and_principles = () =>
        {
            _principlesRaised.ShouldBeTrue();
            _practicesRaised.ShouldBeTrue();
        };
    }
}
