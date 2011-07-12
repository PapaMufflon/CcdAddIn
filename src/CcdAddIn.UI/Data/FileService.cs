using System.IO;
using System.Reflection;
using NLog;

namespace CcdAddIn.UI.Data
{
    class FileService : IFileService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private static string _myDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Bootstrapper)).Location);

        public Stream OpenAsStream(string fileName)
        {
            _logger.Trace("Open file {0}", fileName);
            var file = Path.Combine(_myDirectory, fileName);

            return File.Open(file, FileMode.OpenOrCreate);
        }
    }
}