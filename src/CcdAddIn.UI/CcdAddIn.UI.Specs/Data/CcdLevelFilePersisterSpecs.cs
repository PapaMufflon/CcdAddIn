using System.Collections.Generic;
using System.IO;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Data;
using Machine.Fakes;
using Machine.Specifications;

namespace CcdAddIn.UI.Specs.Data
{
    class CcdLevelFilePersisterSpecs
    {
        [Subject(typeof(CcdLevelFilePersister))]
        public class Given_an_empty_list_of_CcdLevels_When_saving_the_list : WithSubject<CcdLevelFilePersister>
        {
            Establish context = () => Subject = new CcdLevelFilePersister(new MemoryFileService());

            Because of = () => Subject.Save(new List<CcdLevel>());

            It should_be_an_empty_list_again_after_loading = () => Subject.Load().ShouldContainOnly(new List<CcdLevel>());
        }

        [Subject(typeof(CcdLevelFilePersister))]
        public class Given_a_list_of_CcdLevels_When_saving_the_list : WithSubject<CcdLevelFilePersister>
        {
            private static List<CcdLevel> _levels;

            Establish context = () =>
            {
                _levels = new List<CcdLevel>
                {
                    new CcdLevel(Level.Black),
                    new CcdLevel(Level.Red)
                };

                _levels[1].Practices[0].EvaluationValue = 37;

                Subject = new CcdLevelFilePersister(new MemoryFileService());
            };

            Because of = () => Subject.Save(_levels);

            It should_be_the_same_list_again_after_loading = () => Subject.Load().ShouldContainOnly(_levels);
        }

        [Subject(typeof(CcdLevelFilePersister))]
        public class Given_no_file_with_persisted_data_When_loading_the_levels : WithSubject<CcdLevelFilePersister>
        {
            private static List<CcdLevel> _loadedList;

            Establish context = () => Subject = new CcdLevelFilePersister(new MemoryFileService());

            Because of = () => _loadedList = Subject.Load();

            It should_return_an_empty_list = () => _loadedList.ShouldBeEmpty();
        }
    }

    class MemoryFileService : IFileService
    {
        private byte[] _buffer = new byte[32767];

        public Stream OpenAsStream(string fileName)
        {
            return new MemoryStream(_buffer, true);
        }
    }
}