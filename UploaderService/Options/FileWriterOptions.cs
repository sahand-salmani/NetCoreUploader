using System.Linq;
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
        public FileWriterOptions()
        {
        }

        public FileWriterOptions(IFormFile file)
        {
            var options = new UploadFileOption();
            File = file;
            Path = options.SubDirectories.Aggregate(options.RootDirectory, System.IO.Path.Combine);
            FileName = file.FileName;
            FileType = System.IO.Path.GetExtension(file.FileName);
        }
        public FileWriterOptions(IFormFile file, string path, string fileName, string fileType, int size)
        {
            Path = path;
            FileName = fileName;
            FileType = fileType;
            File = file;
        }

        public string Path { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public IFormFile File { get; set; }


    }
}
