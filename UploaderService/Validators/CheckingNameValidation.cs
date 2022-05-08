using System.IO;
using Microsoft.AspNetCore.Http;
using UploaderService.Helpers;
using UploaderService.Options;
using UploaderService.Results;

namespace UploaderService.Validators
{
    public class CheckingNameValidation : IUploadValidation
    {
        private readonly IPathHandler _pathHandler;
        private readonly IExtensionHandler _extensionHandler;

        public CheckingNameValidation(IPathHandler pathHandler,
                                      IExtensionHandler extensionHandler)
        {
            _pathHandler = pathHandler;
            _extensionHandler = extensionHandler;
        }


        public Response<ValidationResult> Validate(IFormFile file, IUploadFileOption options)
        {
            if (options.IsChangingNameAllowed)
            {
                return new Response<ValidationResult>()
                {
                    Success = true
                };
            }

            //var path = _pathHandler.CombineRootAndSubDirectories(options.RootDirectory, options.SubDirectories);
            var path = _pathHandler.MakePath(options.DirectoryMaker);

            if (options.IsDividedByFormat)
            {
                path = Path.Combine(path, _extensionHandler.FindExtension(file.FileName));
            }
            return CheckFileExistsValidation(file, path);
        }

        protected virtual Response<ValidationResult> CheckFileExistsValidation(IFormFile file, string path)
        {
            var filePath = Path.Combine(path, file.FileName);

            var result = new Response<ValidationResult>()
            {
                Success = !File.Exists(filePath)
            };


            if (result.Success) return result;

            result.Messages.Add("File already exists and changing name is not allowed");
            return result;

        }
    }
}
