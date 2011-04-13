using System.Collections.Generic;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Data;
using Machine.Fakes;
using Machine.Specifications;

namespace CcdAddIn.UI.Test
{
    public class Given_a_file_with_wrong_content_When_creating_the_repository : WithSubject<Repository>
    {
        Establish context = () =>
        {
            The<IFileService>()
                .WhenToldTo(x => x.OpenAsString("repository"))
                .Return("wrong content");
        };

        Because of = () => { };

        It should_throw_an_exception = () => Catch.Exception(() => Subject.GetRetrospectives()).ShouldNotBeNull();
    }

    public class Given_a_file_without_history_When_querying_the_repository : WithSubject<Repository>
    {
        Establish context = () =>
        {
            var noHistory = "<Repository />";

            The<IFileService>()
                .WhenToldTo(x => x.OpenAsString("repository"))
                .Return(noHistory);
        };

        Because of = () => { };

        It should_return_an_empty_list = () => Subject.GetRetrospectives().ShouldBeEmpty();
    }

    public class Given_a_file_with_empty_history_When_querying_the_repository : WithSubject<Repository>
    {
        Establish context = () =>
        {
            var emptyHistory = "<Repository><History /></Repository>";

            The<IFileService>()
                .WhenToldTo(x => x.OpenAsString("repository"))
                .Return(emptyHistory);
        };

        Because of = () => { };

        It should_return_an_empty_list = () => Subject.GetRetrospectives().ShouldBeEmpty();
    }

    public class Given_a_file_with_one_retrospective_When_querying_the_repository : WithSubject<Repository>
    {
        Establish context = () =>
        {
            var oneRetrospective = "<Repository><History><Retrospective Level=\"Red\">" +
                                   "<Item Name=\"DoNotRepeatYourself\" Value=\"50\"/>" +
                                   "<Item Name=\"KeepItSimpleStupid\" Value=\"50\"/>" +
                                   "<Item Name=\"VorsichtVorOptimierungen\" Value=\"50\"/>" +
                                   "<Item Name=\"FavorCompositionOverInheritance\" Value=\"50\"/>" +
                                   "<Item Name=\"Pfadfinderregel\" Value=\"60\"/>" +
                                   "<Item Name=\"RootCauseAnalysis\" Value=\"60\"/>" +
                                   "<Item Name=\"Versionskontrolle\" Value=\"60\"/>" +
                                   "<Item Name=\"EinfacheRefaktorisierungen\" Value=\"60\"/>" +
                                   "<Item Name=\"TäglichReflektieren\" Value=\"60\"/>" +
                                   "</Retrospective></History></Repository>";

            The<IFileService>()
                .WhenToldTo(x => x.OpenAsString("repository"))
                .Return(oneRetrospective);
        };

        Because of = () => { };

        It should_return_this_retrospective = () =>
        {
            var expectedLevel = new CcdLevel(Level.Red);

            foreach (var principle in expectedLevel.Principles)
                principle.EvaluationValue = 50;

            foreach (var practice in expectedLevel.Practices)
                practice.EvaluationValue = 60;

            var expected = new List<CcdLevel> {expectedLevel};

            Subject.GetRetrospectives().ShouldContainOnly(expected);
        };
    }

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
}
