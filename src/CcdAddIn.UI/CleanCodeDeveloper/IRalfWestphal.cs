using System.Collections.Generic;

namespace CcdAddIn.UI.CleanCodeDeveloper
{
    public interface IRalfWestphal
    {
        bool ShouldAdvance(List<CcdLevel> retrospectives);
    }
}