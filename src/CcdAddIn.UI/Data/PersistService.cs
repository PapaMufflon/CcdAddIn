using CcdAddIn.UI.CleanCodeDeveloper;

namespace CcdAddIn.UI.Data
{
    public class PersistService
    {
        public PersistService(IRepository repository, CcdLevel currentLevel)
        {
            currentLevel.Advanced += (s, e) => repository.SaveChanges();
        }
    }
}