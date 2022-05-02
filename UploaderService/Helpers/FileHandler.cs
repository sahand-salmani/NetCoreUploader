using System.IO;

namespace UploaderService.Helpers
{
    public interface IFileExistenceHandler
    {
        bool CheckFileExists(string path);
    }
    public class FileExistenceHandler : IFileExistenceHandler
    {
        public bool CheckFileExists(string path)
        {
            return File.Exists(path);
        }
    }
}
