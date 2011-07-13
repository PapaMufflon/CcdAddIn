using System;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using CcdAddIn.UI.ViewModels;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.Specs.ViewModels
{
    class CcdLevelsViewModelSpecs
    {
        [Subject(typeof(CcdLevelsViewModel))]
        public class Given_a_non_black_level_When_not_doing_a_retrospective : WithSubject<CcdLevelsViewModel>
        {
            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(new BeginRetrospectiveEvent());

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(new AdviceGivenEvent());

                Subject = new CcdLevelsViewModel(The<IEventAggregator>(), new CcdLevel(Level.Red));
            };

            Because of = () => { };

            It should_not_show_the_evaluation_controls = () => Subject.EvaluationVisible.ShouldBeFalse();
        }

        [Subject(typeof(CcdLevelsViewModel))]
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
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(new AdviceGivenEvent());

                Subject = new CcdLevelsViewModel(The<IEventAggregator>(), new CcdLevel(Level.Red));
            };

            Because of = () => _beginRetrospectiveEvent.Publish(true);

            It should_switch_to_retrospective_mode = () => Subject.EvaluationVisible.ShouldBeTrue();
        }

        [Subject(typeof(CcdLevelsViewModel))]
        public class Given_a_retrospective_in_progress_When_finishing_the_retrospective : WithSubject<CcdLevelsViewModel>
        {
            private static bool _raised;
            private static CcdLevel _currentLevel = new CcdLevel(Level.Red);

            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(new BeginRetrospectiveEvent());

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(new AdviceGivenEvent());

                var retrospectiveDoneEvent = new RetrospectiveDoneEvent();
                retrospectiveDoneEvent.Subscribe(x => _raised = true);

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<RetrospectiveDoneEvent>())
                    .Return(retrospectiveDoneEvent);

                Subject = new CcdLevelsViewModel(The<IEventAggregator>(), _currentLevel);
            };

            Because of = () => Subject.RetrospectiveDoneCommand.Execute(null);

            It should_stop_retrospective_mode = () => Subject.EvaluationVisible.ShouldBeFalse();
            It should_raise_a_show_advice_event = () => _raised.ShouldBeTrue();
            It should_set_the_timestamp_of_the_retrospective = () => DateTime.Now.Subtract(_currentLevel.TimeOfRetrospective).TotalSeconds.ShouldBeCloseTo(1, 1);
        }

        [Subject(typeof(CcdLevelsViewModel))]
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
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(new AdviceGivenEvent());

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
}
