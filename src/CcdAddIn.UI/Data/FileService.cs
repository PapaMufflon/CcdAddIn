namespace CcdAddIn.UI.Data
{
    class FileService : IFileService
    {
        public void CreateNewFile(string fileName)
        {
            System.IO.File.Create(fileName);
        }
    }
}