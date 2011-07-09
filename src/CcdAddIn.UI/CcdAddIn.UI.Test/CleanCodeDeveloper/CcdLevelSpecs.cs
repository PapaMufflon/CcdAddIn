using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CcdAddIn.UI.CleanCodeDeveloper;
using Machine.Fakes;
using Machine.Specifications;
using Machine.Specifications.Model;

namespace CcdAddIn.UI.Test.CleanCodeDeveloper
{
    class CcdLevelSpecs
    {
        public class Given_a_CcdLevel_When_comparing_it_with_another : WithSubject<CcdLevel>
        {
            Establish context = () =>
            {
                Subject = new CcdLevel(Level.Red);

                foreach (var practice in Subject.Practices)
                    practice.EvaluationValue = 50;

                foreach (var principle in Subject.Principles)
                    principle.EvaluationValue = 60;
            };

            Because of = () => { };

            It should_be_equal_if_the_level_and_the_practices_and_principles_are_equal = () =>
            {
                var expected = new CcdLevel(Level.Red);

                foreach (var practice in expected.Practices)
                    practice.EvaluationValue = 50;

                foreach (var principle in expected.Principles)
                    principle.EvaluationValue = 60;

                Subject.ShouldEqual(expected);
            };
        }

        public class Given_a_red_level_When_advancing : WithSubject<CcdLevel>
        {
            Establish context = () => Subject = new CcdLevel(Level.Red);

            Because of = () => Subject.Advance();

            It should_be_at_the_orange_level = () => Subject.Level.ShouldEqual(Level.Orange);
            It should_provide_the_corresponding_principles = () => Subject.Principles[0].NameAsString.ShouldEqual(Resources.Resource.SingleLevelOfAbstraction);
        }

        public class Given_a_black_level_When_advancing_to_the_next_level : WithSubject<CcdLevel>
        {
            private static bool _raised;

            Establish context = () => Subject = new CcdLevel(Level.Black);

            Because of = () =>
            {
                Subject.Advanced += (s, e) => _raised = true;
                Subject.Advance();
            };

            It should_raise_an_advanced_event = () => _raised.ShouldBeTrue();
        }

        public class Given_a_CcdLevel_When_Cloning_it : WithSubject<CcdLevel>
        {
            private static CcdLevel _clonedLevel;

            Establish context = () =>
            {
                Subject = new CcdLevel(Level.Orange);
                Subject.Practices[0].EvaluationValue = 13;
                Subject.Principles[0].EvaluationValue = 37;
            };

            Because of = () => _clonedLevel = Subject.Clone();

            It should_copy_all_principles_and_practices = () =>
            {
                _clonedLevel.Practices.ShouldContainOnly(Subject.Practices);
                _clonedLevel.Principles.ShouldContainOnly(Subject.Principles);
            };
            It should_copy_the_actual_level = () => _clonedLevel.Level.ShouldEqual(Subject.Level);
        }
    }
}
