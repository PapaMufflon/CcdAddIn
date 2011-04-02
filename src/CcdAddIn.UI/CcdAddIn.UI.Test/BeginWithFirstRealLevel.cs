using CcdAddIn.UI.Communication;
using CcdAddIn.UI.ViewModels;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.Test
{
    public class Given_the_initial_level_is_black_when_going_to_the_red_level : WithSubject<StartViewModel>
    {
        private static bool _raised; 

        Establish context = () =>
        {
            var changeLevelEvent = new ChangeLevelEvent();

            changeLevelEvent.Subscribe(x =>
            {
                if (x == CcdLevel.Red) _raised = true;
            });
            
            The<IEventAggregator>()
                .WhenToldTo(x => x.GetEvent<ChangeLevelEvent>())
                .Return(changeLevelEvent);
        };

        Because of = () => Subject.GoToRedLevelCommand.Execute(null);

        It a_change_to_red_level_event_is_raised = () => _raised.ShouldBeTrue();
    }

    //public class Given_the_current_level_is_black_when_looking_at_the_principles_and_practices : WithSubject<CcdLevelsViewModel>
    //{
    //    Establish context = () => Subject.CurrentLevel.ShouldEqual(CcdLevel.Black);

    //    Because of = () => { };

    //    It there_should_be_no_principles = () => Subject.Principles.Count.ShouldEqual(0);
    //    It there_should_be_no_practices = () => Subject.Practices.Count.ShouldEqual(0);
    //}

    //public class Given_ : WithSubject<CcdLevelsViewModel>
    //{
    //    Establish context = () => Subject.CurrentLevel.ShouldEqual(CcdLevel.Black);

    //    Because of = () => { };

    //    It there_should_be_no_principles = () => Subject.Principles.Count.ShouldEqual(0);
    //    It there_should_be_no_practices = () => Subject.Practices.Count.ShouldEqual(0);
    //}

    //public class Given_the_first_level_is_black_when_browsing_forwards : WithSubject<CcdLevelViewModel>
    //{
    //    Establish context = () => Subject.CurrentLevel.ShouldEqual(CcdLevel.Black);

    //    Because of = () => Subject.BrowseForward();

    //    It the_next_one_should_be_red = () => Subject.CurrentLevel.ShouldEqual(CcdLevel.Red);
    //}
}