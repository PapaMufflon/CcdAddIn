using System;
using System.Collections.Generic;
using System.Linq;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Data;
using Machine.Fakes;
using Machine.Specifications;
using Rhino.Mocks;

namespace CcdAddIn.UI.Test
{
    public class LoadHistory
    {
        public class Given_a_file_with_wrong_content_When_creating_the_repository
        {
            private static IFileService _fileService;
            Establish context = () => _fileService = MockRepository.GenerateStub<IFileService>();

            Because of = () => _fileService.Stub(x => x.OpenAsString("repository")).Return("wrong content");

            It should_throw_an_exception = () => Catch.Exception(() => new Repository(_fileService)).ShouldBeOfType(typeof(InvalidOperationException));
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

            It should_begin_at_the_black_level = () => Subject.CurrentLevel.Level.ShouldEqual(Level.Black);
            It should_have_no_retrospectives = () => Subject.Retrospectives.Count.ShouldEqual(0);
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

            It should_begin_at_the_black_level = () => Subject.CurrentLevel.Level.ShouldEqual(Level.Black);
            It should_have_no_retrospectives = () => Subject.Retrospectives.Count.ShouldEqual(0);
        }

        public class Given_a_file_with_one_retrospective_When_querying_the_repository : WithSubject<Repository>
        {
            Establish context = () =>
            {
                var oneRetrospective = "<Repository><History><Retrospective Level=\"Red\">" +
                                       "<Item Name=\"DoNotRepeatYourself\" Value=\"50\"/>" +
                                       "<Item Name=\"KeepItSimpleStupid\" Value=\"50\"/>" +
                                       "<Item Name=\"BewareOptimizations\" Value=\"50\"/>" +
                                       "<Item Name=\"FavorCompositionOverInheritance\" Value=\"50\"/>" +
                                       "<Item Name=\"BoyscoutRule\" Value=\"60\"/>" +
                                       "<Item Name=\"RootCauseAnalysis\" Value=\"60\"/>" +
                                       "<Item Name=\"VersionControl\" Value=\"60\"/>" +
                                       "<Item Name=\"SimpleRefactorizations\" Value=\"60\"/>" +
                                       "<Item Name=\"DailyReflection\" Value=\"60\"/>" +
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

                var expected = new List<CcdLevel> { expectedLevel };

                Subject.Retrospectives.ShouldContainOnly(expected);
            };
            It should_begin_at_the_red_level = () => Subject.CurrentLevel.Level.ShouldEqual(Level.Red);
        }

        public class Given_a_file_with_two_retrospectives_When_querying_the_repository : WithSubject<Repository>
        {
            Establish context = () =>
            {
                var twoRetrospectives = "<Repository><History><Retrospective Level=\"Red\">" +
                                       "<Item Name=\"DoNotRepeatYourself\" Value=\"50\"/>" +
                                       "<Item Name=\"KeepItSimpleStupid\" Value=\"50\"/>" +
                                       "<Item Name=\"BewareOptimizations\" Value=\"50\"/>" +
                                       "<Item Name=\"FavorCompositionOverInheritance\" Value=\"50\"/>" +
                                       "<Item Name=\"BoyscoutRule\" Value=\"60\"/>" +
                                       "<Item Name=\"RootCauseAnalysis\" Value=\"60\"/>" +
                                       "<Item Name=\"VersionControl\" Value=\"60\"/>" +
                                       "<Item Name=\"SimpleRefactorizations\" Value=\"60\"/>" +
                                       "<Item Name=\"DailyReflection\" Value=\"60\"/>" +
                                       "</Retrospective><Retrospective Level=\"Red\">" +
                                       "<Item Name=\"DoNotRepeatYourself\" Value=\"1\"/>" +
                                       "<Item Name=\"KeepItSimpleStupid\" Value=\"2\"/>" +
                                       "<Item Name=\"BewareOptimizations\" Value=\"3\"/>" +
                                       "<Item Name=\"FavorCompositionOverInheritance\" Value=\"4\"/>" +
                                       "<Item Name=\"BoyscoutRule\" Value=\"5\"/>" +
                                       "<Item Name=\"RootCauseAnalysis\" Value=\"6\"/>" +
                                       "<Item Name=\"VersionControl\" Value=\"7\"/>" +
                                       "<Item Name=\"SimpleRefactorizations\" Value=\"8\"/>" +
                                       "<Item Name=\"DailyReflection\" Value=\"9\"/>" +
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
            It should_begin_at_the_red_level = () => Subject.CurrentLevel.Level.ShouldEqual(Level.Red);
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