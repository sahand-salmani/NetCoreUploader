using System.IO;
using UploaderService.Helpers;
using UploaderService.Options;

namespace UploaderService.UploadOptionsProcessors
{
    public class UploadOptionFileExistenceProcessor : IUploadOptionProcessor
    {
        private readonly IFileExistenceHandler _fileExistenceHandler;
        private readonly IFileNameHandler _fileNameHandler;

        public UploadOptionFileExistenceProcessor(IFileExistenceHandler fileExistenceHandler,
            IFileNameHandler fileNameHandler)
        {
            _fileExistenceHandler = fileExistenceHandler;
            _fileNameHandler = fileNameHandler;
        }
        public byte Order { get; set; } = 30;
        public void Process(IUploadFileOption uploadOptions, ref FileWriterOptions writerOptions)
        {
            var path = writerOptions.Path;
            var fileName = writerOptions.FileName;
            var fullPath = Path.Combine(path, fileName);

            if (_fileExistenceHandler.CheckFileExists(fullPath) && uploadOptions.IsChangingNameAllowed)
            {
                fileName = _fileNameHandler.FindNewExistingName(fileName, path);
            }

            writerOptions.FileName = fileName;
        }
    }
}
