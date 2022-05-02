using Microsoft.AspNetCore.Http;
using UploaderService.Options;
using UploaderService.Results;

namespace UploaderService.Validators
{
    public interface IUploadValidation
    {
        Response<ValidationResult> Validate(IFormFile file, IUploadFileOption options);
    }
}
