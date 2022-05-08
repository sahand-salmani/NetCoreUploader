using System.Collections.Generic;
using static UploaderService.Helpers.DirectoryFactory;

namespace UploaderService.Options
{
    public interface IUploadFileOption
    {
        //public string RootDirectory { get; set; }
        //public List<string> SubDirectories { get; set; }
        public bool IsDividedByFormat { get; set; }
        public bool IsChangingNameAllowed { get; set; }
        public string FileName { get; set; }
        public int MaxAllowedSizeInKb { get; set; }
        public List<AllowedFormatsToUpload> AllowedFormats { get; set; }
        public CustomDirectory DirectoryMaker { get; set; }

    }
    public class UploadFileOption : IUploadFileOption
    {
        //public string RootDirectory { get; set; } = string.Empty;
        //public List<string> SubDirectories { get; set; } = new();
        public bool IsDividedByFormat { get; set; } = false;
        public bool IsChangingNameAllowed { get; set; } = true;
        public string FileName { get; set; }
        public int MaxAllowedSizeInKb { get; set; } = int.MaxValue;
        public List<AllowedFormatsToUpload> AllowedFormats { get; set; }
            = new()
            {
                AllowedFormatsToUpload.All
            };

        public CustomDirectory DirectoryMaker { get; set; }
    }
}
