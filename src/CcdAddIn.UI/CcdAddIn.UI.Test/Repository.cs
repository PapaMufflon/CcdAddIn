using System;
using System.Collections.Generic;
using System.Xml.Linq;
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

        private static List<CcdLevel> _foo;
        It should_throw_an_exception = () => Catch.Exception(() => _foo = Subject.Retrospectives).ShouldNotBeNull();
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

        It should_return_an_empty_list = () => Subject.Retrospectives.ShouldBeEmpty();
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

        It should_return_an_empty_list = () => Subject.Retrospectives.ShouldBeEmpty();
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

            Subject.Retrospectives.ShouldContainOnly(expected);
        };
    }

    public class Given_a_file_with_two_retrospectives_When_querying_the_repository : WithSubject<Repository>
    {
        Establish context = () =>
        {
            var twoRetrospectives = "<Repository><History><Retrospective Level=\"Red\">" +
                                   "<Item Name=\"DoNotRepeatYourself\" Value=\"50\"/>" +
                                   "<Item Name=\"KeepItSimpleStupid\" Value=\"50\"/>" +
                                   "<Item Name=\"VorsichtVorOptimierungen\" Value=\"50\"/>" +
                                   "<Item Name=\"FavorCompositionOverInheritance\" Value=\"50\"/>" +
                                   "<Item Name=\"Pfadfinderregel\" Value=\"60\"/>" +
                                   "<Item Name=\"RootCauseAnalysis\" Value=\"60\"/>" +
                                   "<Item Name=\"Versionskontrolle\" Value=\"60\"/>" +
                                   "<Item Name=\"EinfacheRefaktorisierungen\" Value=\"60\"/>" +
                                   "<Item Name=\"TäglichReflektieren\" Value=\"60\"/>" +
                                   "</Retrospective><Retrospective Level=\"Red\">" +
                                   "<Item Name=\"DoNotRepeatYourself\" Value=\"1\"/>" +
                                   "<Item Name=\"KeepItSimpleStupid\" Value=\"2\"/>" +
                                   "<Item Name=\"VorsichtVorOptimierungen\" Value=\"3\"/>" +
                                   "<Item Name=\"FavorCompositionOverInheritance\" Value=\"4\"/>" +
                                   "<Item Name=\"Pfadfinderregel\" Value=\"5\"/>" +
                                   "<Item Name=\"RootCauseAnalysis\" Value=\"6\"/>" +
                                   "<Item Name=\"Versionskontrolle\" Value=\"7\"/>" +
                                   "<Item Name=\"EinfacheRefaktorisierungen\" Value=\"8\"/>" +
                                   "<Item Name=\"TäglichReflektieren\" Value=\"9\"/>" +
                                   "</Retrospective></History></Repository>";

            The<IFileService>()
                .WhenToldTo(x => x.OpenAsString("repository"))
                .Return(twoRetrospectives);
        };

        Because of = () => { };

        It should_return_these_retrospectives = () =>
        {
            var firstExpectedLevel = new CcdLevel(Level.Red);

            foreach (var principle in firstExpectedLevel.Principles)
                principle.EvaluationValue = 50;

            foreach (var practice in firstExpectedLevel.Practices)
                practice.EvaluationValue = 60;

            var secondExpectedLevel = new CcdLevel(Level.Red);
            secondExpectedLevel.Principles[0].EvaluationValue = 1;
            secondExpectedLevel.Principles[1].EvaluationValue = 2;
            secondExpectedLevel.Principles[2].EvaluationValue = 3;
            secondExpectedLevel.Principles[3].EvaluationValue = 4;
            secondExpectedLevel.Practices[0].EvaluationValue = 5;
            secondExpectedLevel.Practices[1].EvaluationValue = 6;
            secondExpectedLevel.Practices[2].EvaluationValue = 7;
            secondExpectedLevel.Practices[3].EvaluationValue = 8;
            secondExpectedLevel.Practices[4].EvaluationValue = 9;

            var expected = new List<CcdLevel> { firstExpectedLevel, secondExpectedLevel };

            Subject.Retrospectives.ShouldContainOnly(expected);
        };
    }

    public class Given_no_retrospectives_When_saving_changes : WithSubject<Repository>
    {
        private static XDocument _emptyHistory = new XDocument(
            new XElement("Repository",
                         new XElement("History")));

        Establish context = () =>
        {
            The<IFileService>()
                .WhenToldTo(x => x.OpenAsString("repository"))
                .Return(_emptyHistory.ToString());
        };

        Because of = () => Subject.SaveChanges();

        It should_save_just_the_frame = () =>
            The<IFileService>()
                .WasToldTo(x => x.WriteTo(_emptyHistory.ToString(), "repository"));
    }

    public class Given_a_single_retrospective_When_saving_changes : WithSubject<Repository>
    {
        Establish context = () =>
        {
            var emptyHistory = "<Repository><History /></Repository>";

            The<IFileService>()
                .WhenToldTo(x => x.OpenAsString("repository"))
                .Return(emptyHistory);
        };

        Because of = () =>
        {
            var level = new CcdLevel(Level.Red);

            foreach (var principle in level.Principles)
                principle.EvaluationValue = 50;

            foreach (var practice in level.Practices)
                practice.EvaluationValue = 60;

            Subject.Retrospectives.Add(level);
            Subject.SaveChanges();
        };

        It should_save_it = () =>
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
                .WasToldTo(x => x.WriteTo(oneRetrospective, "repository"));
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
