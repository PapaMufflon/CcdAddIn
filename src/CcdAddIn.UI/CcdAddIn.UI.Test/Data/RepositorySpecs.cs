using System.Collections.Generic;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Data;
using Machine.Fakes;
using Machine.Specifications;

namespace CcdAddIn.UI.Test.Data
{
    class RepositorySpecs
    {
        [Subject(typeof(Repository))]
        public class Given_no_saved_retrospectives_When_creating_the_repository : WithSubject<Repository>
        {
            Establish context = () => The<IPersister<List<CcdLevel>>>()
                                        .WhenToldTo(x => x.Load())
                                        .Return(new List<CcdLevel>());

            Because of = () => { };

            It should_provide_the_black_level_as_current_level = () => Subject.CurrentLevel.ShouldEqual(new CcdLevel(Level.Black));
        }

        [Subject(typeof(Repository))]
        public class Given_some_saved_retrospectives_When_creating_the_repository : WithSubject<Repository>
        {
            private static List<CcdLevel> _retrospectives = new List<CcdLevel>
            {
                new CcdLevel(Level.Orange),
                new CcdLevel(Level.Red)
            };

            Establish context = () => The<IPersister<List<CcdLevel>>>()
                                        .WhenToldTo(x => x.Load())
                                        .Return(_retrospectives);

            Because of = () => { };

            It should_provide_these_retrospectives = () => Subject.Retrospectives.ShouldContainOnly(_retrospectives);
            It should_provide_the_last_retrospective_as_current_level = () => Subject.CurrentLevel.ShouldEqual(new CcdLevel(Level.Orange));
        }

        [Subject(typeof(Repository))]
        public class Given_no_history_When_saving_changes : WithSubject<Repository>
        {
            Establish context = () => The<IPersister<List<CcdLevel>>>()
                .WhenToldTo(x => x.Load())
                .Return(new List<CcdLevel>());

            Because of = () => Subject.SaveChanges();

            It should_only_save_the_current_level = () => The<IPersister<List<CcdLevel>>>()
                .WasToldTo(x => x.Save(new List<CcdLevel>{new CcdLevel(Level.Black)}));
        }

        [Subject(typeof(Repository))]
        public class Given_one_historical_retrospective_When_saving_changes : WithSubject<Repository>
        {
            Establish context = () => The<IPersister<List<CcdLevel>>>()
                .WhenToldTo(x => x.Load())
                .Return(new List<CcdLevel> { new CcdLevel(Level.Black) });

            Because of = () => Subject.SaveChanges();

            It should_save_the_old_retrospective_and_the_current_one = () => The<IPersister<List<CcdLevel>>>()
                .WasToldTo(x => x.Save(new List<CcdLevel>
                                           {
                                               new CcdLevel(Level.Black),
                                               new CcdLevel(Level.Black)
                                           }));
        }

        [Subject(typeof(Repository))]
        public class Given_no_history_When_advancing_a_level_between_saving_changes : WithSubject<Repository>
        {
            Establish context = () => The<IPersister<List<CcdLevel>>>()
                .WhenToldTo(x => x.Load())
                .Return(new List<CcdLevel>());

            Because of = () =>
            {
                var level = Subject.CurrentLevel;

                Subject.SaveChanges();
                level.Advance();

                Subject.SaveChanges();
            };

            It should_save_the_old_retrospective_and_the_current_one = () => The<IPersister<List<CcdLevel>>>()
                .WasToldTo(x => x.Save(new List<CcdLevel>
                                           {
                                               new CcdLevel(Level.Red),
                                               new CcdLevel(Level.Black)
                                           }));
        }
    }
}