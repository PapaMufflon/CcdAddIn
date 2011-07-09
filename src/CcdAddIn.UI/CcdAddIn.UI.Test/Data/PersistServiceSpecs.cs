using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using CcdAddIn.UI.Data;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.Test.Data
{
    class PersistServiceSpecs
    {
        [Subject(typeof(PersistService))]
        public class Given_a_retrospective_in_progress_When_finishing_the_retrospective2 : WithSubject<PersistService>
        {
            private static AdviceGivenEvent _adviceGivenEvent = new AdviceGivenEvent();

            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(_adviceGivenEvent);

                Subject = new PersistService(The<IRepository>(), The<IEventAggregator>(), new CcdLevel(Level.Black));
            };

            Because of = () => _adviceGivenEvent.Publish(null);

            It should_persist_the_progress = () => The<IRepository>().WasToldTo(x => x.SaveChanges());
        }

        [Subject(typeof(PersistService))]
        public class Given_a_black_level_When_advancing_to_the_next_one : WithSubject<PersistService>
        {
            private static CcdLevel _currentLevel = new CcdLevel(Level.Black);

            Establish context = () =>
            {
                The<IEventAggregator>()
                    .WhenToldTo(x => x.GetEvent<AdviceGivenEvent>())
                    .Return(new AdviceGivenEvent());

                Subject = new PersistService(The<IRepository>(), The<IEventAggregator>(), _currentLevel);
            };

            Because of = () => _currentLevel.Advance();

            It should_save_the_retrospectives = () => The<IRepository>().WasToldTo(x => x.SaveChanges());
        }
    }
}
