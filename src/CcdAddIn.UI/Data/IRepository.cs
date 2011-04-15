using System.Collections.Generic;
using CcdAddIn.UI.CleanCodeDeveloper;

namespace CcdAddIn.UI.Data
{
    public interface IRepository
    {
        List<CcdLevel> Retrospectives { get; set; }

        void SaveChanges();
    }
}
