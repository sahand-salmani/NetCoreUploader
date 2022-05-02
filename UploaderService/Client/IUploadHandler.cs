using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UploaderService.Initializer;
using UploaderService.Options;
using UploaderService.Results;
using UploaderService.Validators;

namespace UploaderService.Client
{
    public interface IUploadHandler
    {
        Task<Response<UploadFileResult>> UploadFile(IFormFile file);
        Task<Response<UploadFileResult>> UploadFile(IFormFile file, Action<UploadFileOption> options);
        Task<Response<UploadFileResult>> UploadFile(IFormFile file, Action<UploadFileOption> options,
            params IUploadValidation[] validations);
    }
    public class UploadHandler : IUploadHandler
    {
        private readonly IUploadInitializer _initializer;

        public UploadHandler(IUploadInitializer initializer)
        {
            _initializer = initializer;
        }
        public async Task<Response<UploadFileResult>> UploadFile(IFormFile file)
        {
            return await _initializer.Initialize(file);
        }

        public async Task<Response<UploadFileResult>> UploadFile(IFormFile file, Action<UploadFileOption> options)
        {
            return await _initializer.Initialize(file, options);
        }

        public async Task<Response<UploadFileResult>> UploadFile(IFormFile file, Action<UploadFileOption> options, params IUploadValidation[] validations)
        {
            return await _initializer.Initialize(file, options, validations);
        }
    }
}
