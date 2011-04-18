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
                (File.Create(fileName)).Close();

            var content = File.ReadAllText(fileName);
            _logger.Trace("Read {0}", content);

            return content;
        }

        public void WriteTo(string content, string fileName)
        {
            _logger.Trace("Write {0} to file {1}", content, fileName);
            File.WriteAllText(fileName, content);
        }
    }
}