using System;
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
            private static ChangeLevelEvent _changeLevelEvent;

            Establish context = () =>
            {
                _changeLevelEvent = new ChangeLevelEvent();

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<ChangeLevelEvent>())
                    .Return(_changeLevelEvent);

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<RetrospectiveInProgressEvent>())
                    .Return(new RetrospectiveInProgressEvent());

                Subject = new HeaderViewModel(The<IEventAggregator>());
            };

            Because of = () => _changeLevelEvent.Publish(Level.Red);

            It should_be_possible = () => Subject.RetrospectiveAvailable.ShouldBeTrue();
        }

        public class Given_a_non_black_level_When_clicking_on_retrospective : WithSubject<HeaderViewModel>
        {
            private static bool _raised;

            Establish context = () =>
            {
                var beginRetrospectiveEvent = new RetrospectiveInProgressEvent();

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<RetrospectiveInProgressEvent>())
                    .Return(beginRetrospectiveEvent);

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<ChangeLevelEvent>())
                    .Return(new ChangeLevelEvent());

                beginRetrospectiveEvent.Subscribe(x => _raised = true);

                Subject = new HeaderViewModel(The<IEventAggregator>());
            };

            Because of = () => Subject.BeginRetrospectiveCommand.Execute(null);

            It should_raise_a_begin_retrospective_event = () => _raised.ShouldBeTrue();
        }

        public class Given_a_retrospective_in_progress_When_the_retrospective_is_finished : WithSubject<HeaderViewModel>
        {
            private static RetrospectiveInProgressEvent _retrospectiveInProgressEvent;

            Establish context = () =>
            {
                _retrospectiveInProgressEvent = new RetrospectiveInProgressEvent();

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<RetrospectiveInProgressEvent>())
                    .Return(_retrospectiveInProgressEvent);

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<ChangeLevelEvent>())
                    .Return(new ChangeLevelEvent());

                Subject = new HeaderViewModel(The<IEventAggregator>());
            };

            Because of = () =>
            {
                Subject.BeginRetrospectiveCommand.Execute(null);
                Subject.BeginRetrospectiveCommand.CanExecute(null).ShouldBeFalse();

                _retrospectiveInProgressEvent.Publish(false);
            };

            It should_enable_the_begin_retrospective_command_again = () => Subject.BeginRetrospectiveCommand.CanExecute(null).ShouldBeTrue();
        }

        public class Given_a_non_black_level_When_not_doing_a_retrospective : WithSubject<CcdLevelsViewModel>
        {
            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<RetrospectiveInProgressEvent>())
                    .Return(new RetrospectiveInProgressEvent());

                Subject = new CcdLevelsViewModel(The<IEventAggregator>());
            };

            Because of = () => { };

            It should_not_show_the_evaluation_controls = () => Subject.EvaluationVisible.ShouldBeFalse();
        }

        public class Given_a_non_black_level_When_wanting_to_do_a_retrospective : WithSubject<CcdLevelsViewModel>
        {
            private static RetrospectiveInProgressEvent _retrospectiveInProgressEvent;

            Establish context = () =>
            {
                _retrospectiveInProgressEvent = new RetrospectiveInProgressEvent();

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<RetrospectiveInProgressEvent>())
                    .Return(_retrospectiveInProgressEvent);

                Subject = new CcdLevelsViewModel(The<IEventAggregator>());
            };

            Because of = () => _retrospectiveInProgressEvent.Publish(true);

            It should_switch_to_retrospective_mode = () => Subject.EvaluationVisible.ShouldBeTrue();
        }

        public class Given_a_retrospective_in_progress_When_finishing_the_retrospective : WithSubject<CcdLevelsViewModel>
        {
            private static bool _raised;

            Establish context = () =>
            {
                var retrospectiveInProgressEvent = new RetrospectiveInProgressEvent();

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<RetrospectiveInProgressEvent>())
                    .Return(retrospectiveInProgressEvent);

                var showAdviceEvent = new ShowAdviceEvent();
                showAdviceEvent.Subscribe(x => _raised = true);

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<ShowAdviceEvent>())
                    .Return(showAdviceEvent);

                Subject = new CcdLevelsViewModel(The<IEventAggregator>());

                retrospectiveInProgressEvent.Publish(true);
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