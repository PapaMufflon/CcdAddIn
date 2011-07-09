using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using CcdAddIn.UI.ViewModels;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.Test.ViewModels
{
    class HeaderViewModelSpecs
    {
        public class Given_the_black_level_When_advancing_to_the_red_level : WithSubject<HeaderViewModel>
        {
            private static CcdLevel _currentLevel = new CcdLevel(Level.Black);

            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(new BeginRetrospectiveEvent());

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(new AdviceGivenEvent());

                Subject = new HeaderViewModel(The<IEventAggregator>(), _currentLevel);
            };

            Because of = () => _currentLevel.Advance();

            It should_be_possible_to_do_a_retrospective = () => Subject.RetrospectiveAvailable.ShouldBeTrue();
        }

        public class Given_a_non_black_level_When_doing_a_retrospective : WithSubject<HeaderViewModel>
        {
            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(new BeginRetrospectiveEvent());

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(new AdviceGivenEvent());

                Subject = new HeaderViewModel(The<IEventAggregator>(), new CcdLevel(Level.Red));
            };

            Because of = () => { };

            It should_be_possible = () => Subject.RetrospectiveAvailable.ShouldBeTrue();
        }

        public class Given_a_non_black_level_When_clicking_on_retrospective : WithSubject<HeaderViewModel>
        {
            private static bool _raised;

            Establish context = () =>
            {
                var beginRetrospectiveEvent = new BeginRetrospectiveEvent();
                beginRetrospectiveEvent.Subscribe(x => _raised = true);

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(beginRetrospectiveEvent);

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(new AdviceGivenEvent());

                Subject = new HeaderViewModel(The<IEventAggregator>(), new CcdLevel(Level.Black));
            };

            Because of = () => Subject.BeginRetrospectiveCommand.Execute(null);

            It should_raise_a_begin_retrospective_event = () => _raised.ShouldBeTrue();
        }

        public class Given_a_retrospective_in_progress_When_advancing_to_the_white_level : WithSubject<HeaderViewModel>
        {
            private static CcdLevel _currentLevel;
            private static AdviceGivenEvent _adviceGivenEvent = new AdviceGivenEvent();

            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(new BeginRetrospectiveEvent());

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(_adviceGivenEvent);

                _currentLevel = new CcdLevel(Level.Blue);

                Subject = new HeaderViewModel(The<IEventAggregator>(), _currentLevel);
            };

            Because of = () =>
            {
                Subject.BeginRetrospectiveCommand.Execute(null);

                _currentLevel.Advance();
                _adviceGivenEvent.Publish(null);
            };

            It should_not_be_possible_to_do_a_retrospective = () => Subject.RetrospectiveAvailable.ShouldBeFalse();
        }

        public class Given_a_retrospective_in_progress_When_advancing_a_level_after_ending_the_retrospective : WithSubject<HeaderViewModel>
        {
            private static CcdLevel _currentLevel = new CcdLevel(Level.Red);
            private static AdviceGivenEvent _adviceGivenEvent = new AdviceGivenEvent();

            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(new BeginRetrospectiveEvent());

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(_adviceGivenEvent);

                Subject = new HeaderViewModel(The<IEventAggregator>(), _currentLevel);
            };

            Because of = () =>
            {
                Subject.BeginRetrospectiveCommand.Execute(null);

                _currentLevel.Advance();
                _adviceGivenEvent.Publish(null);
            };

            It should_show_the_begin_retrospective_command_again = () => Subject.RetrospectiveAvailable.ShouldBeTrue();
        }

        public class Given_a_retrospective_in_progress_When_not_advancing_a_level_after_ending_the_retrospective : WithSubject<HeaderViewModel>
        {
            private static AdviceGivenEvent _adviceGivenEvent = new AdviceGivenEvent();
            private static CcdLevel _currentLevel = new CcdLevel(Level.Red);

            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<BeginRetrospectiveEvent>())
                    .Return(new BeginRetrospectiveEvent());

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(_adviceGivenEvent);

                Subject = new HeaderViewModel(The<IEventAggregator>(), _currentLevel);
            };

            Because of = () =>
            {
                Subject.BeginRetrospectiveCommand.Execute(null);

                _adviceGivenEvent.Publish(null);
            };

            It should_show_the_begin_retrospective_command_again = () => Subject.RetrospectiveAvailable.ShouldBeTrue();
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
}
