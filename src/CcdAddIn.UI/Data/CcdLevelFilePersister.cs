using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using CcdAddIn.UI.CleanCodeDeveloper;

namespace CcdAddIn.UI.Data
{
    public class CcdLevelFilePersister : IPersister<List<CcdLevel>>
    {
        private readonly IFileService _fileService;
        private const string FileName = "repository";

        public CcdLevelFilePersister(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void Save(List<CcdLevel> objectToBeSaved)
        {
            var stream = _fileService.OpenAsStream(FileName);// File.Open(FileName, FileMode.Create);
            var serializer = new BinaryFormatter();

            serializer.Serialize(stream, objectToBeSaved);

            stream.Close();
        }

        public List<CcdLevel> Load()
        {
            var stream = _fileService.OpenAsStream(FileName);// File.Open(FileName, FileMode.Open);
            var serializer = new BinaryFormatter();

            try
            {
                return (List<CcdLevel>)serializer.Deserialize(stream);
            }
            catch (SerializationException)
            {
                return new List<CcdLevel>();
            }
            finally
            {
                stream.Close();
            }
        }
    }
}