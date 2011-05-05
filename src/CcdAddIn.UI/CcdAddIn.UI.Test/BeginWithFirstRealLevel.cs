﻿using CcdAddIn.UI.CleanCodeDeveloper;
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
            var changeLevelEvent = new GoToLevelEvent();

            changeLevelEvent.Subscribe(x =>
            {
                if (x == Level.Red) _raised = true;
            });
            
            The<IEventAggregator>()
                .WhenToldTo(x => x.GetEvent<GoToLevelEvent>())
                .Return(changeLevelEvent);
        };

        Because of = () => Subject.GoToRedLevelCommand.Execute(null);

        It should_raise_a_change_to_red_level_event = () => _raised.ShouldBeTrue();
    }

    public class Given_the_initial_level_is_black_when_wanting_to_do_a_retrospective : WithSubject<HeaderViewModel>
    {
        Establish context = () =>
        {
            The<IEventAggregator>()
                .WhenToldTo(x => x.GetEvent<GoToLevelEvent>())
                .Return(new GoToLevelEvent());

            The<IEventAggregator>()
                .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                .Return(new BeginRetrospectiveEvent());

            The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<EndRetrospectiveEvent>())
                    .Return(new EndRetrospectiveEvent());
        };

        Because of = () => { };

        It should_not_be_available = () => Subject.RetrospectiveAvailable.ShouldBeFalse();
    }
}