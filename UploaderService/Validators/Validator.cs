using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using UploaderService.Options;
using UploaderService.Results;

namespace UploaderService.Validators
{
    public interface IValidator
    {
        Response<ValidationResult> Validate(IFormFile file, IUploadFileOption options, params IUploadValidation[] validations);
    }
    public class Validator : IValidator
    {
        private readonly Queue<IUploadValidation> _validations = new();

        public Response<ValidationResult> Validate(IFormFile file, IUploadFileOption options, params IUploadValidation[] validations)
        {
            var validator = new Validator();
            foreach (var uploadValidation in validations)
            {
                validator._validations.Enqueue(uploadValidation);
            }
            return ValidateFile(file, options, validator._validations);
        }

        private static Response<ValidationResult> ValidateFile(IFormFile file, IUploadFileOption options, Queue<IUploadValidation> validations)
        {
            var result = new Response<ValidationResult>()
            {
                Success = true
            };


            foreach (var uploadValidation in validations)
            {
                var validationResult = uploadValidation.Validate(file, options);
                result.Success = result.Success && validationResult.Success;

                if (!validationResult.Success)
                {
                    result.AddError(validationResult.Messages);
                }
            }

            return result;
        }
    }
}
