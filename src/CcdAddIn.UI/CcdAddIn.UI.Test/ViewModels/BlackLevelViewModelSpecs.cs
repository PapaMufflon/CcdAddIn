using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.ViewModels;
using Machine.Fakes;
using Machine.Specifications;

namespace CcdAddIn.UI.Test.ViewModels
{
    class BlackLevelViewModelSpecs
    {
        [Subject(typeof(BlackLevelViewModel))]
        public class Given_the_initial_level_is_black_when_going_to_the_red_level : WithSubject<BlackLevelViewModel>
        {
            private static CcdLevel _currentLevel = new CcdLevel(Level.Black);

            Establish context = () =>
            {
                Subject = new BlackLevelViewModel(_currentLevel);
            };

            Because of = () => Subject.GoToRedLevelCommand.Execute(null);

            It should_change_the_current_level_to_red = () => _currentLevel.Level.ShouldEqual(Level.Red);
        }
    }
}
