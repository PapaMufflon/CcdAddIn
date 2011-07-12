using System.Collections.Generic;
using System.Linq;
using CcdAddIn.UI.CleanCodeDeveloper;
using NLog;

namespace CcdAddIn.UI.Data
{
    public class Repository : IRepository
    {
        public List<CcdLevel> Retrospectives { get; set; }

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IPersister<List<CcdLevel>> _persister;
        private CcdLevel _currentLevel;

        public Repository(IPersister<List<CcdLevel>> persister)
        {
            _logger.Trace("Creating repository");
            _persister = persister;

            Initialize();
        }

        private void Initialize()
        {
            Retrospectives = _persister.Load();
            var lastRetrospective = Retrospectives.FirstOrDefault();

            if (lastRetrospective == null)
                _currentLevel = new CcdLevel(Level.Black);
            else
                _currentLevel = lastRetrospective.Clone();
        }

        public CcdLevel CurrentLevel
        {
            get { return _currentLevel; }
        }

        public void SaveChanges()
        {
            Retrospectives.Insert(0, _currentLevel.Clone());
            _persister.Save(Retrospectives);
        }
    }
}