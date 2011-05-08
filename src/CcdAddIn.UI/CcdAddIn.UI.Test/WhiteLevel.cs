using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using CcdAddIn.UI.Views;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.Test
{
    public class WhiteLevel
    {
        public class Given_the_white_level_When_RestartingTheCycle : WithSubject<WhiteLevelViewModel>
        {
            private static bool _raised;
            private static GoToLevelEvent _goToLevelEvent = new GoToLevelEvent();

            Establish context = () =>
            {
                _goToLevelEvent.Subscribe(x =>
                {
                    if (x == Level.Red)
                        _raised = true;
                });

                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<GoToLevelEvent>())
                    .Return(_goToLevelEvent);
            };

            Because of = () => Subject.RestartCommand.Execute(null);

            It should_raise_a_go_to_red_level_event = () => _raised.ShouldBeTrue();
        }
    }
}
