using UploaderService.Helpers;
using UploaderService.Options;

namespace UploaderService.UploadOptionsProcessors
{
    public class UploadOptionNameProcessor : IUploadOptionProcessor
    {
        private readonly IExtensionHandler _extensionHandler;

        public UploadOptionNameProcessor(IExtensionHandler extensionHandler)
        {
            _extensionHandler = extensionHandler;
        }
        public byte Order { get; set; } = 10;
        public void Process(IUploadFileOption uploadOptions, ref FileWriterOptions writerOptions)
        {
            var ext = _extensionHandler.FindExtension(writerOptions.File.FileName);

            if (!string.IsNullOrEmpty(uploadOptions.FileName))
            {
                writerOptions.FileName = uploadOptions.FileName + "." + ext;
            }
            else
            {
                writerOptions.FileName = writerOptions.File.FileName;
            }

            writerOptions.FileType = ext;
        }
    }
}
