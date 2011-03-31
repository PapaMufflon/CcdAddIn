using CcdAddIn.UI.ViewModels;
using Machine.Fakes;
using Machine.Specifications;

namespace CcdAddIn.UI.Test
{
    public class Given_the_first_level_is_black_when_browsing_forwards : WithSubject<CcdLevelViewModel>
    {
        Establish context = () => { };

        Because of = () => Subject.BrowseForward();

        It the_next_one_should_be_red = () => Subject.CurrentLevel.ShouldEqual(CcdLevel.Red);
    }
}