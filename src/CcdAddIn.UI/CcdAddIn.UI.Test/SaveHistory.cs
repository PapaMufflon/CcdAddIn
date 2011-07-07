using System.IO;
using System.Xml.Linq;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Data;
using Machine.Fakes;
using Machine.Specifications;

namespace CcdAddIn.UI.Test
{
    public class SaveHistory
    {
        public class Given_the_current_retrospective_When_saving_changes : WithSubject<Repository>
        {
            Establish context = () =>
            {
                var emptyHistory = "<Repository><History /></Repository>";

                The<IFileService>()
                    .WhenToldTo(x => x.OpenAsString("repository"))
                    .Return(emptyHistory);
            };

            Because of = () => Subject.SaveChanges();
            
            It should_save_it = () =>
            {
                var oneRetrospective = "<Repository><History><Retrospective Level=\"Black\" />" +
                                       "</History></Repository>";

                var xmlFormat = (XDocument.Load(new StringReader(oneRetrospective))).ToString();
                The<IFileService>()
                    .WasToldTo(x => x.WriteTo(xmlFormat, "repository"));
            };
        }

        public class Given_an_loaded_retrospective_and_a_current_one_When_saving_changes : WithSubject<Repository>
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

            Because of = () => Subject.SaveChanges();

            It should_save_both = () =>
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
                                       "</Retrospective><Retrospective Level=\"Red\">" +
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

                var xmlFormat = (XDocument.Load(new StringReader(oneRetrospective))).ToString();
                The<IFileService>()
                    .WasToldTo(x => x.WriteTo(xmlFormat, "repository"));
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
}