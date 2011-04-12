using System.Collections.Generic;
using CcdAddIn.UI.CleanCodeDeveloper;

namespace CcdAddIn.UI.Data
{
    public class Repository : IRepository
    {
        public Repository(IFileService fileService)
        {
            fileService.CreateNewFile("repository");
        }

        public List<CcdLevel> GetRetrospectives()
        {
            return new List<CcdLevel>();
        }
    }
}