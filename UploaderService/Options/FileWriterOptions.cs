using Microsoft.AspNetCore.Http;

namespace UploaderService.Options
{
    public interface IFileWriterOptions
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public IFormFile File { get; set; }
    }
    public class FileWriterOptions : IFileWriterOptions
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public IFormFile File { get; set; }
    }
}
