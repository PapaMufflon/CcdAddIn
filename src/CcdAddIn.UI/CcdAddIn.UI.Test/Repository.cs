using System.Collections.Generic;
using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Data;
using Machine.Fakes;
using Machine.Specifications;

namespace CcdAddIn.UI.Test
{
    public class Given_no_file_to_load_from_When_querying_retrospectives : WithSubject<Repository>
    {
        private static List<CcdLevel> _retrospectives;

        Establish context = () => { };

        Because of = () => _retrospectives = Subject.GetRetrospectives();

        It should_create_a_new_file = () => The<IFileService>().WasToldTo(x => x.CreateNewFile("repository"));
        It should_return_an_empty_list = () => _retrospectives.ShouldBeEmpty();
    }
}
