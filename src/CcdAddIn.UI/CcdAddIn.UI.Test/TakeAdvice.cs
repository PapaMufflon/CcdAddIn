using System.Collections.Generic;
using CcdAddIn.UI.CleanCodeDeveloper;
using Machine.Fakes;
using Machine.Specifications;

namespace CcdAddIn.UI.Test
{
    public class Given_a_first_start_When_asking_to_advance_to_the_next_level : WithSubject<RalfWestphal>
    {
        private static bool _shouldAdvance;

        private Establish context = () =>
        {
            var retrospectives = new List<CcdLevel>
            {
                new CcdLevel(Level.Red)
            };

            Subject = new RalfWestphal(retrospectives);
        };

        private Because of = () => _shouldAdvance = Subject.ShouldAdvance();

        private It should_be_false = () => _shouldAdvance.ShouldBeFalse();
    }
}
