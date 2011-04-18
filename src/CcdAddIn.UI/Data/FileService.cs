using System.IO;
using NLog;

namespace CcdAddIn.UI.Data
{
    class FileService : IFileService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public string OpenAsString(string fileName)
        {
            _logger.Trace("Open file {0}", fileName);

            if (!File.Exists(fileName))
                File.Create(fileName);

            return File.ReadAllText(fileName);
        }

        public void WriteTo(string content, string fileName)
        {
            _logger.Trace("Write {0} to file {1}", content, fileName);
            File.WriteAllText(fileName, content);
        }
    }
}