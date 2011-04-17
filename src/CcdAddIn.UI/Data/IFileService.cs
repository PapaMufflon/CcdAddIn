namespace CcdAddIn.UI.Data
{
    public interface IFileService
    {
        string OpenAsString(string fileName);
        void WriteTo(string content, string fileName);
    }
}