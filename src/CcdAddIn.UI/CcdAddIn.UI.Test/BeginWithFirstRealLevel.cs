using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using CcdAddIn.UI.Data;
using CcdAddIn.UI.ViewModels;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.Test
{
    public class Given_the_initial_level_is_black_when_going_to_the_red_level : WithSubject<BlackLevelViewModel>
    {
        private static CcdLevel _currentLevel = new CcdLevel(Level.Black);

        Establish context = () =>
        {
            Subject = new BlackLevelViewModel(_currentLevel);
        };

        Because of = () => Subject.GoToRedLevelCommand.Execute(null);

        It should_change_the_current_level_to_red = () => _currentLevel.Level.ShouldEqual(Level.Red);
    }

    public class Given_a_black_level_When_advancing_to_the_next_one : WithSubject<PersistService>
    {
        private static CcdLevel _currentLevel = new CcdLevel(Level.Black);

        Establish context = () =>
        {
            The<IEventAggregator>()
                .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                .Return(new AdviceGivenEvent());

            Subject = new PersistService(The<IRepository>(), The<IEventAggregator>(), _currentLevel);
        };

        Because of = () => _currentLevel.Advance();

        It should_save_the_retrospectives = () => The<IRepository>().WasToldTo(x => x.SaveChanges());
    }

    public class Given_the_initial_level_is_black_when_wanting_to_do_a_retrospective : WithSubject<HeaderViewModel>
    {
        Establish context = () =>
        {
            The<IEventAggregator>()
                .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                .Return(new BeginRetrospectiveEvent());

            The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(new AdviceGivenEvent());

            Subject = new HeaderViewModel(The<IEventAggregator>(), new CcdLevel(Level.Black));
        };

        Because of = () => { };

        It should_not_be_available = () => Subject.RetrospectiveAvailable.ShouldBeFalse();
    }
}