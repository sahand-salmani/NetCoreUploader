using System.IO;

namespace UploaderService.Helpers
{
    public interface IFileNameHandler
    {
        string FindNewExistingName(string fileName, string path);
    }
    public class FileNameHandler : IFileNameHandler
    {
        private readonly IExtensionHandler _extensionHandler;
        private readonly IFileExistenceHandler _fileExistenceHandler;

        public FileNameHandler(IExtensionHandler extensionHandler,
                               IFileExistenceHandler fileExistenceHandler)
        {
            _extensionHandler = extensionHandler;
            _fileExistenceHandler = fileExistenceHandler;
        }
        public string FindNewExistingName(string fileName, string path)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }


            var newPath = Path.Combine(path, fileName);
            var newFileName = fileName;
            var ext = _extensionHandler.FindExtension(fileName);
            var fileNameWithOutExtension = _extensionHandler.GetFileNameWithoutExtension(fileName);
            if (string.IsNullOrEmpty(ext) || string.IsNullOrEmpty(fileNameWithOutExtension))
            {
                return null;
            }
            var i = 1;
            while (_fileExistenceHandler.CheckFileExists(newPath))
            {
                var tempFileName = fileNameWithOutExtension;
                tempFileName += "-" + i;
                newFileName = tempFileName + "." + ext;
                newPath = Path.Combine(path, newFileName);
                i++;
            }

            return newFileName;
        }
    }
}
