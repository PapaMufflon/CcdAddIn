using System.Collections.Generic;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using CcdAddIn.UI.Data;
using CcdAddIn.UI.ViewModels;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.Specs.ViewModels
{
    class AdviceViewModelSpecs
    {
        [Subject(typeof(AdviceViewModel))]
        public class Given_a_wish_to_take_an_advice_When_navigating_to_it : WithSubject<AdviceViewModel>
        {
            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(new AdviceGivenEvent());

                Subject = new AdviceViewModel(The<IEventAggregator>(), The<IRepository>(), The<IRalfWestphal>(), new CcdLevel(Level.Black));
            };

            Because of = () => Subject.OnNavigatedTo(null);

            It should_requery_Ralf_Westphal = () => The<IRalfWestphal>().WasToldTo(x => x.ShouldAdvance(null));
        }

        [Subject(typeof(AdviceViewModel))]
        public class Given_an_negative_advice_When_taking_it : WithSubject<AdviceViewModel>
        {
            private static bool _raised;
            private static CcdLevel _currentLevel = new CcdLevel(Level.Red);

            Establish context = () =>
            {
                var adviceGivenEvent = new AdviceGivenEvent();
                adviceGivenEvent.Subscribe(x => _raised = true);

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(adviceGivenEvent);

                The<IRepository>().Retrospectives = new List<CcdLevel>();

                Subject = new AdviceViewModel(The<IEventAggregator>(), The<IRepository>(), The<IRalfWestphal>(), _currentLevel);
            };

            Because of = () => Subject.TakeAdviceCommand.Execute(null);

            It should_raise_an_end_retrospective_event = () => _raised.ShouldBeTrue();
            It should_stay_at_the_same_level = () => _currentLevel.Level.ShouldEqual(Level.Red);
        }

        [Subject(typeof(AdviceViewModel))]
        public class Given_retrospectives_justifying_a_level_up_When_querying_for_advice : WithSubject<AdviceViewModel>
        {
            Establish context = () =>
            {
                The<IRalfWestphal>()
                    .WhenToldTo(x => x.ShouldAdvance(null))
                    .Return(true);

                Subject = new AdviceViewModel(The<IEventAggregator>(), The<IRepository>(), The<IRalfWestphal>(), new CcdLevel(Level.Red));
            };

            Because of = () => { };

            It should_be_a_positive_advice = () => Subject.Advice.ShouldEqual(Resources.Resource.PositiveAdvice);
            It should_activate_stay_at_same_level_choice = () => Subject.CanAdvance.ShouldBeTrue();
        }

        [Subject(typeof(AdviceViewModel))]
        public class Given_retrospectives_justifying_a_level_up_When_taking_an_advice : WithSubject<AdviceViewModel>
        {
            private static CcdLevel _currentLevel = new CcdLevel(Level.Red);

            Establish context = () =>
            {
                The<IRalfWestphal>()
                    .WhenToldTo(x => x.ShouldAdvance(null))
                    .Return(true);

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(new AdviceGivenEvent());

                Subject = new AdviceViewModel(The<IEventAggregator>(), The<IRepository>(), The<IRalfWestphal>(), _currentLevel);
            };

            Because of = () => Subject.TakeAdviceCommand.Execute(null);

            It should_advance_to_the_next_level = () => _currentLevel.Level.ShouldEqual(Level.Orange);
        }

        [Subject(typeof(AdviceViewModel))]
        public class Given_retrospectives_not_justifying_a_level_up_When_denying_the_advice : WithSubject<AdviceViewModel>
        {
            private static CcdLevel _currentLevel = new CcdLevel(Level.Red);

            Establish context = () =>
            {
                var retrospectives = new List<CcdLevel>();

                for (int i = 0; i < 21; i++)
                {
                    var level = new CcdLevel(Level.Red);

                    foreach (var practice in level.Practices)
                        practice.EvaluationValue = 0;

                    foreach (var principle in level.Principles)
                        principle.EvaluationValue = 0;

                    retrospectives.Add(level);
                }

                The<IRepository>().Retrospectives = retrospectives;

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(new AdviceGivenEvent());

                Subject = new AdviceViewModel(The<IEventAggregator>(), The<IRepository>(), The<IRalfWestphal>(), _currentLevel);
            };

            Because of = () => Subject.DenyAdviceCommand.Execute(null);

            It should_advance_to_the_next_level = () => _currentLevel.Level.ShouldEqual(Level.Orange);
        }

        [Subject(typeof(AdviceViewModel))]
        public class Given_retrospectives_not_justifying_a_level_up_When_querying_for_an_advice : WithSubject<AdviceViewModel>
        {
            Establish context = () =>
            {
                var retrospectives = new List<CcdLevel>();

                for (int i = 0; i < 21; i++)
                {
                    var level = new CcdLevel(Level.Red);

                    foreach (var practice in level.Practices)
                        practice.EvaluationValue = 0;

                    foreach (var principle in level.Principles)
                        principle.EvaluationValue = 0;

                    retrospectives.Add(level);
                }

                The<IRepository>().Retrospectives = retrospectives;

                Subject = new AdviceViewModel(The<IEventAggregator>(), The<IRepository>(), The<IRalfWestphal>(), new CcdLevel(Level.Red));
            };

            Because of = () => { };

            It should_be_a_negative_advice = () => Subject.Advice.ShouldEqual(Resources.Resource.NegativeAdvice);
            It should_activate_advance_to_next_level_choice = () => Subject.CanAdvance.ShouldBeFalse();
        }
    }
}
