using System;
using System.IO;
using System.Threading.Tasks;
using UploaderService.Options;
using UploaderService.Results;

namespace UploaderService.FileWriters
{
    public interface IFileWriter
    {
        Task<Response<UploadFileResult>> WriteFile(FileWriterOptions fileWriterOptions);
    }
    public class FileWriter : IFileWriter
    {
        public async Task<Response<UploadFileResult>> WriteFile(FileWriterOptions fileWriterOptions)
        {
            try
            {
                var fullPath = Path.Combine(fileWriterOptions.Path, fileWriterOptions.FileName);
                await using var ms = new FileStream(fullPath, FileMode.Create);
                await fileWriterOptions.File.CopyToAsync(ms);
                var uploadFileResult = new UploadFileResult(fileWriterOptions.Path, fullPath, fileWriterOptions.FileName,
                    fileWriterOptions.FileType, (int)(fileWriterOptions.File.Length / 1024));
                var result = Response.Success(uploadFileResult, "Upload Succeeded");
                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
