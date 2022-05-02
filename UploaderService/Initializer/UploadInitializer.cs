using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UploaderService.FileWriters;
using UploaderService.Options;
using UploaderService.Results;
using UploaderService.UploadOptionsProcessors;
using UploaderService.Validators;

namespace UploaderService.Initializer
{
    public interface IUploadInitializer
    {
        Task<Response<UploadFileResult>> Initialize(IFormFile file);
        Task<Response<UploadFileResult>> Initialize(IFormFile file, Action<UploadFileOption> options);
        Task<Response<UploadFileResult>> Initialize(IFormFile file, Action<UploadFileOption> options,
            params IUploadValidation[] validations);
    }
    public class UploadInitializer : IUploadInitializer
    {
        private readonly IValidationHandler _validationHandler;
        private readonly IFileWriter _fileWriter;
        private readonly IProcessorHandler _processorHandler;

        public UploadInitializer(IValidationHandler validationHandler,
                                 IFileWriter fileWriter,
                                 IProcessorHandler processorHandler)
        {
            _validationHandler = validationHandler;
            _fileWriter = fileWriter;
            _processorHandler = processorHandler;
        }
        public async Task<Response<UploadFileResult>> Initialize(IFormFile file)
        {
            Response<ValidationResult> validationResult = _validationHandler.ValidateWithAllValidation(file);
            if (!validationResult.Success)
            {
                return Response.Fail<UploadFileResult>(validationResult.Messages);
            }

            var fileWriterOptions = _processorHandler.ProcessAll(file, new UploadFileOption());
            if (!fileWriterOptions.Success)
            {
                return Response.Fail<UploadFileResult>(fileWriterOptions.Messages);
            }

            return await _fileWriter.WriteFile(fileWriterOptions.Data);
        }

        public async Task<Response<UploadFileResult>> Initialize(IFormFile file, Action<UploadFileOption> options)
        {
            var opt = new UploadFileOption();
            options.Invoke(opt);
            Response<ValidationResult> validationResult = _validationHandler.ValidateWithAllValidation(file, opt);
            if (!validationResult.Success)
            {
                return Response.Fail<UploadFileResult>(validationResult.Messages);
            }

            var fileWriterOptions = _processorHandler.ProcessAll(file, options);
            if (!fileWriterOptions.Success)
            {
                return Response.Fail<UploadFileResult>(fileWriterOptions.Messages);
            }

            return await _fileWriter.WriteFile(fileWriterOptions.Data);
        }

        public async Task<Response<UploadFileResult>> Initialize(IFormFile file, Action<UploadFileOption> options, params IUploadValidation[] validations)
        {
            var opt = new UploadFileOption();
            options.Invoke(opt);
            Response<ValidationResult> validationResult = _validationHandler.ValidateWithCustomValidation(file,opt, validations);

            if (!validationResult.Success)
            {
                return Response.Fail<UploadFileResult>(validationResult.Messages);
            }

            var fileWriterOptions = _processorHandler.ProcessAll(file, opt);
            if (!fileWriterOptions.Success)
            {
                return Response.Fail<UploadFileResult>(fileWriterOptions.Messages);
            }

            return await _fileWriter.WriteFile(fileWriterOptions.Data);
        }
    }
}
