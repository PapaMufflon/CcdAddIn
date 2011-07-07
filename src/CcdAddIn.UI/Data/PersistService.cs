using CcdAddIn.UI.CleanCodeDeveloper;
using CcdAddIn.UI.Communication;
using Microsoft.Practices.Prism.Events;

namespace CcdAddIn.UI.Data
{
    public class PersistService
    {
        private readonly IRepository _repository;

        public PersistService(IRepository repository, IEventAggregator eventAggregator, CcdLevel currentLevel)
        {
            _repository = repository;

            currentLevel.Advanced += (s, e) => PersistData(null);
            eventAggregator.GetEvent<AdviceGivenEvent>().Subscribe(PersistData);
        }

        private void PersistData(object obj)
        {
            _repository.SaveChanges();
        }
    }
}