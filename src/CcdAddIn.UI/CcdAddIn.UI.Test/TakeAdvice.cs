using System;
using System.Collections.Generic;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using CcdAddIn.UI.Data;
using CcdAddIn.UI.ViewModels;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.Test
{
    public class Given_a_first_start_When_asking_to_advance_to_the_next_level : WithSubject<RalfWestphal>
    {
        private static bool _shouldAdvance;

        Establish context = () => { };

        Because of = () =>
        {
            var retrospectives = new List<CcdLevel>
            {
                new CcdLevel(Level.Red)
            };

            _shouldAdvance = RalfWestphal.ShouldAdvance(retrospectives);
        };

        It should_be_false = () => _shouldAdvance.ShouldBeFalse();
    }

    public class Given_an_advice_When_taking_it : WithSubject<AdviceViewModel>
    {
        private static bool _raised;

        Establish context = () =>
        {
            var retrospectiveInProgressEvent = new RetrospectiveInProgressEvent();
            retrospectiveInProgressEvent.Subscribe(x => _raised = true);

            The<IEventAggregator>()
                .WhenToldTo(x => x.GetEvent<RetrospectiveInProgressEvent>())
                .Return(retrospectiveInProgressEvent);

            The<IRepository>()
                .WhenToldTo(x => x.GetRetrospectives())
                .Return(new List<CcdLevel>());
        };

        Because of = () => Subject.TakeAdviceCommand.Execute(null);

        It should_raise_a_retrospective_finished_event = () => _raised.ShouldBeTrue();
    }

    public class Given_21_consecutive_days_with_all_retrospectives_above_80_percent_When_asking_to_advance_to_the_next_level : WithSubject<RalfWestphal>
    {
        private static bool _shouldAdvance;

        Establish context = () => { };

        Because of = () =>
        {
            var retrospectives = new List<CcdLevel>();

            for (int i = 0; i < 21; i++)
            {
                var level = new CcdLevel(Level.Red);

                foreach (var practice in level.Practices)
                    practice.EvaluationValue = 90;

                foreach (var principle in level.Principles)
                    principle.EvaluationValue = 90;

                retrospectives.Add(level);
            }

            _shouldAdvance = RalfWestphal.ShouldAdvance(retrospectives);
        };

        It should_be_true = () => _shouldAdvance.ShouldBeTrue();
    }

    public class Given_retrospectives_justifying_a_level_up_When_taking_an_advice : WithSubject<AdviceViewModel>
    {
        Establish context = () =>
        {
            var retrospectives = new List<CcdLevel>();

            for (int i = 0; i < 21; i++)
            {
                var level = new CcdLevel(Level.Red);

                foreach (var practice in level.Practices)
                    practice.EvaluationValue = 90;

                foreach (var principle in level.Principles)
                    principle.EvaluationValue = 90;

                retrospectives.Add(level);
            }

            The<IRepository>()
                .WhenToldTo(x => x.GetRetrospectives())
                .Return(retrospectives);
        };

        Because of = () => { };

        It should_be_a_positive_advice = () => Subject.Advice.ShouldEqual(Resources.Resource.PositiveAdvice);
        It should_activate_stay_at_same_level_choice = () => Subject.CanAdvance.ShouldBeTrue();
    }

    public class Given_retrospectives_not_justifying_a_level_up_When_taking_an_advice : WithSubject<AdviceViewModel>
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

            The<IRepository>()
                .WhenToldTo(x => x.GetRetrospectives())
                .Return(retrospectives);
        };

        Because of = () => { };

        It should_be_a_negative_advice = () => Subject.Advice.ShouldEqual(Resources.Resource.NegativeAdvice);
        It should_activate_advance_to_next_level_choice = () => Subject.CanAdvance.ShouldBeFalse();
    }
}
