﻿using System;
using System.Collections.Generic;
using System.Linq;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using CcdAddIn.UI.ViewModels;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.Test
{
    class Retrospective
    {
        public class Given_a_non_black_level_When_doing_a_retrospective : WithSubject<HeaderViewModel>
        {
            private static GoToLevelEvent _goToLevelEvent;

            Establish context = () =>
            {
                _goToLevelEvent = new GoToLevelEvent();

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<GoToLevelEvent>())
                    .Return(_goToLevelEvent);

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(new BeginRetrospectiveEvent());
            };

            Because of = () => _goToLevelEvent.Publish(Level.Red);

            It should_be_possible = () => Subject.RetrospectiveAvailable.ShouldBeTrue();
        }

        public class Given_a_non_black_level_When_clicking_on_retrospective : WithSubject<HeaderViewModel>
        {
            private static bool _raised;

            Establish context = () =>
            {
                var beginRetrospectiveEvent = new BeginRetrospectiveEvent();

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(beginRetrospectiveEvent);

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<GoToLevelEvent>())
                    .Return(new GoToLevelEvent());

                beginRetrospectiveEvent.Subscribe(x => _raised = true);
            };

            Because of = () => Subject.BeginRetrospectiveCommand.Execute(null);

            It should_raise_a_begin_retrospective_event = () => _raised.ShouldBeTrue();
        }

        public class Given_a_retrospective_in_progress_When_the_retrospective_is_finished : WithSubject<HeaderViewModel>
        {
            private static GoToLevelEvent _goToLevelEvent;

            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(new BeginRetrospectiveEvent());

                _goToLevelEvent = new GoToLevelEvent();
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<GoToLevelEvent>())
                    .Return(_goToLevelEvent);
            };

            Because of = () =>
            {
                Subject.BeginRetrospectiveCommand.Execute(null);

                _goToLevelEvent.Publish(Level.Orange);
            };

            It should_show_the_begin_retrospective_command_again = () => Subject.RetrospectiveAvailable.ShouldBeTrue();
        }

        public class Given_a_retrospective_in_progress_When_advancing_to_the_white_level : WithSubject<HeaderViewModel>
        {
            private static GoToLevelEvent _goToLevelEvent;

            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(new BeginRetrospectiveEvent());

                _goToLevelEvent = new GoToLevelEvent();
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<GoToLevelEvent>())
                    .Return(_goToLevelEvent);
            };

            Because of = () =>
            {
                Subject.BeginRetrospectiveCommand.Execute(null);

                _goToLevelEvent.Publish(Level.White);
            };

            It should_not_be_possible_to_do_a_retrospective = () => Subject.RetrospectiveAvailable.ShouldBeFalse();
        }

        public class Given_a_non_black_level_When_not_doing_a_retrospective : WithSubject<CcdLevelsViewModel>
        {
            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(new BeginRetrospectiveEvent());

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<EndRetrospectiveEvent>())
                    .Return(new EndRetrospectiveEvent());
            };

            Because of = () => { };

            It should_not_show_the_evaluation_controls = () => Subject.EvaluationVisible.ShouldBeFalse();
        }

        public class Given_a_non_black_level_When_wanting_to_do_a_retrospective : WithSubject<CcdLevelsViewModel>
        {
            private static BeginRetrospectiveEvent _beginRetrospectiveEvent;

            Establish context = () =>
            {
                _beginRetrospectiveEvent = new BeginRetrospectiveEvent();

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(_beginRetrospectiveEvent);

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<EndRetrospectiveEvent>())
                    .Return(new EndRetrospectiveEvent());
            };

            Because of = () => _beginRetrospectiveEvent.Publish(true);

            It should_switch_to_retrospective_mode = () => Subject.EvaluationVisible.ShouldBeTrue();
        }

        public class Given_a_retrospective_in_progress_When_finishing_the_retrospective : WithSubject<CcdLevelsViewModel>
        {
            private static bool _raised;

            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(new BeginRetrospectiveEvent());

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<EndRetrospectiveEvent>())
                    .Return(new EndRetrospectiveEvent());

                var showAdviceEvent = new ShowAdviceEvent();
                showAdviceEvent.Subscribe(x => _raised = true);

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<ShowAdviceEvent>())
                    .Return(showAdviceEvent);
            };

            Because of = () => Subject.RetrospectiveDoneCommand.Execute(null);

            It should_stop_retrospective_mode = () => Subject.EvaluationVisible.ShouldBeFalse();
            It should_raise_a_show_advice_event = () => _raised.ShouldBeTrue();
        }

        public class Given_a_clean_code_developer_level_When_comparing_the_items_with_the_itemNames : WithFakes
        {
            private static Dictionary<Level, List<Item>> _items = new Dictionary<Level, List<Item>>();
            private static Dictionary<Level, List<ItemName>> _itemNames = new Dictionary<Level, List<ItemName>>();

            Establish context = () =>
            {
                foreach (Level level in Enum.GetValues(typeof(Level)))
                {
                    _items.Add(level, ItemFactory.GetItemsFor(level));
                    _itemNames.Add(level, ItemFactory.GetItemNamesFor(level));
                }
            };

            Because of = () => { };

            It should_have_the_same_items = () =>
            {
                foreach (var key in _items.Keys)
                {
                    _itemNames[key].ShouldContainOnly(_items[key].Select(x => x.Name));
                }
            };
        }
    }
}