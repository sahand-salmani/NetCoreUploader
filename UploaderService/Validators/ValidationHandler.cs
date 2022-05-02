using System;
using Microsoft.AspNetCore.Http;
using UploaderService.Options;
using UploaderService.Results;

namespace UploaderService.Validators
{
    public interface IValidationHandler
    {
        Response<ValidationResult> ValidateWithAllValidation(IFormFile file);
        Response<ValidationResult> ValidateWithAllValidation(IFormFile file, IUploadFileOption options);
        Response<ValidationResult> ValidateWithCustomValidation(IFormFile file, params IUploadValidation[] validations);
        Response<ValidationResult> ValidateWithCustomValidation(IFormFile file, IUploadFileOption options, params IUploadValidation[] validations);

        // TODO ADD VALIDATE WITH ALL VALIDATIONS EXCEPT LATER
        //Response<ValidationResult> ValidateWithAllValidationsExcept(IFormFile file, params IUploadValidation[] validations);
        //Response<ValidationResult> ValidateWithAllValidationsExcept(IFormFile file, IUploadFileOption options, params IUploadValidation[] validations);
    }
    public class ValidationHandler : IValidationHandler
    {
        private readonly IValidationRepository _validationRepository;
        private readonly IValidator _validator;

        public ValidationHandler(IValidationRepository validationRepository,
            IValidator validator)
        {
            _validationRepository = validationRepository;
            _validator = validator;
        }
        public Response<ValidationResult> ValidateWithAllValidation(IFormFile file)
        {
            var options = new UploadFileOption();
            if (file == null)
            {
                return new Response<ValidationResult>().AddError("File was empty");
            }
            var allValidations = _validationRepository.GetAllValidations();

            return _validator.Validate(file, options, allValidations);

        }
        public Response<ValidationResult> ValidateWithAllValidation(IFormFile file, IUploadFileOption options)
        {
            if (file == null)
            {
                return new Response<ValidationResult>().AddError("File was empty");
            }
            var allValidations = _validationRepository.GetAllValidations();
            return _validator.Validate(file, options, allValidations);
        }
        public Response<ValidationResult> ValidateWithCustomValidation(IFormFile file, params IUploadValidation[] validations)
        {
            return file == null ? new Response<ValidationResult>().AddError("File was empty") : _validator.Validate(file, new UploadFileOption(), validations);
        }
        public Response<ValidationResult> ValidateWithCustomValidation(IFormFile file, IUploadFileOption options,
            params IUploadValidation[] validations)
        {
            return file == null ? new Response<ValidationResult>().AddError("File was empty") : _validator.Validate(file, options, validations);
        }

    }
}
