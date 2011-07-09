using System.Collections.Generic;
using CcdAddIn.UI.CleanCodeDeveloper;
using Machine.Fakes;
using Machine.Specifications;

namespace CcdAddIn.UI.Test.CleanCodeDeveloper
{
    class RalfWestphalSpecs
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

                _shouldAdvance = Subject.ShouldAdvance(retrospectives);
            };

            It should_be_false = () => _shouldAdvance.ShouldBeFalse();
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

                _shouldAdvance = Subject.ShouldAdvance(retrospectives);
            };

            It should_be_true = () => _shouldAdvance.ShouldBeTrue();
        }
    }
}
