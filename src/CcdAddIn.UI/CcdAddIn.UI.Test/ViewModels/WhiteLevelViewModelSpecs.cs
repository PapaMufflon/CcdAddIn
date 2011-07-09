using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Views;
using Machine.Fakes;
using Machine.Specifications;

namespace CcdAddIn.UI.Test.ViewModels
{
    class WhiteLevelViewModelSpecs
    {
        [Subject(typeof(WhiteLevelViewModel))]
        public class Given_the_white_level_When_restarting_the_cycle : WithSubject<WhiteLevelViewModel>
        {
            private static CcdLevel _currentLevel = new CcdLevel(Level.White);

            Establish context = () =>
            {
                Subject = new WhiteLevelViewModel(_currentLevel);
            };

            Because of = () => Subject.RestartCommand.Execute(null);

            It should_go_to_the_red_level = () => _currentLevel.Level.ShouldEqual(Level.Red);
        }
    }
}
