using System;

namespace CcdAddIn.UI.Data
{
    class FileService : IFileService
    {
        public string OpenAsString(string fileName)
        {
            return "<foo/>";
        }

        public void WriteTo(string content, string fileName)
        {
            
        }
    }
}