using System.Collections.Generic;

namespace UploaderService.Results
{
    public class UploadFileResult
    {
        public UploadFileResult()
        {
        }

        public UploadFileResult(string url, string fullUrl, string fileName, string type, int size)
        {
            Url = url;
            FullUrl = fullUrl;
            FileName = fileName;
            FileType = type;
            Size = size;
        }
        public string Url { get; set; }
        public string FullUrl { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int Size { get; set; }

    }
}
