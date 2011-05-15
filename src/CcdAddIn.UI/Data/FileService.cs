using System.IO;
using System.Reflection;
using NLog;

namespace CcdAddIn.UI.Data
{
    class FileService : IFileService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private static string _myDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof (Bootstrapper)).Location);

        public string OpenAsString(string fileName)
        {
            _logger.Trace("Open file {0}", fileName);
            var file = Path.Combine(_myDirectory, fileName);

            if (!File.Exists(file))
                (File.Create(file)).Close();

            var content = File.ReadAllText(file);
            _logger.Trace("Reading from {0}: {1}", file, content);

            return content;
        }

        public void WriteTo(string content, string fileName)
        {
            var file = Path.Combine(_myDirectory, fileName);

            _logger.Trace("Writing to file {0}: {1}", file, content);
            File.WriteAllText(file, content);
        }
    }
}