using System.IO;

namespace CcdAddIn.UI.Data
{
    public interface IFileService
    {
        Stream OpenAsStream(string fileName);
    }
}