using Microsoft.AspNetCore.Http;
using UploaderService.Options;
using UploaderService.Results;

namespace UploaderService.Validators
{
    public class SizeValidation : IUploadValidation
    {
        public Response<ValidationResult> Validate(IFormFile file, IUploadFileOption options)
        {
            return SizeValidator(file, options.MaxAllowedSizeInKb);
        }

        
        protected virtual Response<ValidationResult> SizeValidator(IFormFile file, int maxSize)
        {
            var result = new Response<ValidationResult>()
            {
                Success = (file.Length / 1024) < maxSize
            };

            return result.Success ? result : result.AddError("Size of file exceeded the specified limit");
        }
    }
}
