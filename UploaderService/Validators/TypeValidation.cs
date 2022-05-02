using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using UploaderService.Helpers;
using UploaderService.Options;
using UploaderService.Results;

namespace UploaderService.Validators
{
    public class TypeValidation : IUploadValidation
    {
        private readonly IExtensionHandler _extensionHandler;

        public TypeValidation(IExtensionHandler extensionHandler)
        {
            _extensionHandler = extensionHandler;
        }
        public Response<ValidationResult> Validate(IFormFile file, IUploadFileOption options)
        {
            return TypeValidator(file, options.AllowedFormats);
        }

        protected virtual Response<ValidationResult> TypeValidator(IFormFile file, List<AllowedFormatsToUpload> types)
        {
            var ext = _extensionHandler.FindExtension(file.FileName);
            var result = new Response<ValidationResult>();

            if (types.Contains(AllowedFormatsToUpload.All))
            {
                result.Success = true;
                return result;
            }
            result.Success = types.Any(allowedFormatsToUpload => string.Equals(allowedFormatsToUpload.ToString(), ext, StringComparison.CurrentCultureIgnoreCase));

            return result.Success ? result : result.AddError("File type is not a valid type");
        }
    }
}
