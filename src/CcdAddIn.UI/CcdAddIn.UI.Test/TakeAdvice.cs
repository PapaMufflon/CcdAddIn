﻿using System.Collections.Generic;
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

    public class Given_an_negative_advice_When_taking_it : WithSubject<AdviceViewModel>
    {
        private static bool _raised;
        private static CcdLevel _currentLevel = new CcdLevel(Level.Red);

        Establish context = () =>
        {
            var endRetrospectiveEvent = new EndRetrospectiveEvent();
            endRetrospectiveEvent.Subscribe(x => _raised = true);

            The<IEventAggregator>()
                .WhenToldTo(x => x.GetEvent<EndRetrospectiveEvent>())
                .Return(endRetrospectiveEvent);

            The<IRepository>().Retrospectives = new List<CcdLevel>();

            Subject = new AdviceViewModel(The<IEventAggregator>(), The<IRepository>(), _currentLevel);
        };

        Because of = () => Subject.TakeAdviceCommand.Execute(null);

        It should_raise_an_end_retrospective_event = () => _raised.ShouldBeTrue();
        It should_stay_at_the_same_level = () => _currentLevel.Level.ShouldEqual(Level.Red);
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

    public class Given_retrospectives_justifying_a_level_up_When_querying_for_advice : WithSubject<AdviceViewModel>
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

            The<IRepository>().Retrospectives = retrospectives;

            Subject = new AdviceViewModel(The<IEventAggregator>(), The<IRepository>(), new CcdLevel(Level.Red));
        };

        Because of = () => { };

        It should_be_a_positive_advice = () => Subject.Advice.ShouldEqual(Resources.Resource.PositiveAdvice);
        It should_activate_stay_at_same_level_choice = () => Subject.CanAdvance.ShouldBeTrue();
    }

    public class Given_retrospectives_justifying_a_level_up_When_taking_an_advice : WithSubject<AdviceViewModel>
    {
        private static CcdLevel _currentLevel = new CcdLevel(Level.Red);

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

            The<IRepository>().Retrospectives = retrospectives;

            The<IEventAggregator>()
                .WhenToldTo(x => x.GetEvent<EndRetrospectiveEvent>())
                .Return(new EndRetrospectiveEvent());

            Subject = new AdviceViewModel(The<IEventAggregator>(), The<IRepository>(), _currentLevel);
        };

        Because of = () => Subject.TakeAdviceCommand.Execute(null);

        It should_advance_to_the_next_level = () => _currentLevel.Level.ShouldEqual(Level.Orange);
    }

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
                .WhenToldTo(x => x.GetEvent<EndRetrospectiveEvent>())
                .Return(new EndRetrospectiveEvent());

            Subject = new AdviceViewModel(The<IEventAggregator>(), The<IRepository>(), _currentLevel);
        };

        Because of = () => Subject.DenyAdviceCommand.Execute(null);

        It should_advance_to_the_next_level = () => _currentLevel.Level.ShouldEqual(Level.Orange);
    }

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

            Subject = new AdviceViewModel(The<IEventAggregator>(), The<IRepository>(), new CcdLevel(Level.Red));
        };

        Because of = () => { };

        It should_be_a_negative_advice = () => Subject.Advice.ShouldEqual(Resources.Resource.NegativeAdvice);
        It should_activate_advance_to_next_level_choice = () => Subject.CanAdvance.ShouldBeFalse();
    }

    public class Given_a_red_level_When_advancing : WithSubject<CcdLevel>
    {
        Establish context = () => Subject = new CcdLevel(Level.Red);

        Because of = () => Subject.Advance();

        It should_be_at_the_orange_level = () => Subject.Level.ShouldEqual(Level.Orange);
        It should_provide_the_corresponding_principles = () => Subject.Principles[0].NameAsString.ShouldEqual(Resources.Resource.SingleLevelOfAbstraction);
    }
}
