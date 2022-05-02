using UploaderService.Options;

namespace UploaderService.UploadOptionsProcessors
{
    public interface IUploadOptionProcessor
    {
        public byte Order { get; set; }
        void Process(IUploadFileOption uploadOptions, ref FileWriterOptions writerOptions);
    }
}
