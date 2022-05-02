using System.IO;
using UploaderService.Helpers;
using UploaderService.Options;

namespace UploaderService.UploadOptionsProcessors
{
    public class UploadOptionDirectoryProcessor : IUploadOptionProcessor
    {
        private readonly IExtensionHandler _extensionHandler;
        private readonly IPathHandler _pathHandler;
        private readonly IDirectoryHandler _directoryHandler;

        public UploadOptionDirectoryProcessor(IExtensionHandler extensionHandler,
            IPathHandler pathHandler,
            IDirectoryHandler directoryHandler)
        {
            _extensionHandler = extensionHandler;
            _pathHandler = pathHandler;
            _directoryHandler = directoryHandler;
        }

        public byte Order { get; set; } = 20;

        public void Process(IUploadFileOption uploadOptions, ref FileWriterOptions writerOptions)
        {
            var path = _pathHandler.CombineRootAndSubDirectories(uploadOptions.RootDirectory,
                uploadOptions.SubDirectories);

            if (uploadOptions.IsDividedByFormat)
            {
                var extension = _extensionHandler.FindExtension(writerOptions.File.FileName);
                path = Path.Combine(path, extension);
            }

            _directoryHandler.CheckOrCreateDirectory(path);

            writerOptions.Path = path;
        }
    }
}
