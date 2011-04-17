using System.IO;

namespace CcdAddIn.UI.Data
{
    class FileService : IFileService
    {
        public string OpenAsString(string fileName)
        {
            if (!File.Exists(fileName))
                File.Create(fileName);

            return File.ReadAllText(fileName);
        }

        public void WriteTo(string content, string fileName)
        {
            File.WriteAllText(fileName, content);
        }
    }
}