using System;
using Microsoft.AspNetCore.Http;
using UploaderService.Options;
using UploaderService.Results;

namespace UploaderService.UploadOptionsProcessors
{
    public interface IProcessorHandler
    {
        Response<FileWriterOptions> ProcessAll(IFormFile file, Action<UploadFileOption> uploadFileOption);
        Response<FileWriterOptions> ProcessAll(IFormFile file, UploadFileOption uploadFileOption);
    }
    public class ProcessorHandler : IProcessorHandler
    {
        private readonly IProcessorRepository _processorRepository;

        public ProcessorHandler(IProcessorRepository processorRepository)
        {
            _processorRepository = processorRepository;
        }
        public Response<FileWriterOptions> ProcessAll(IFormFile file, Action<UploadFileOption> uploadFileOption)
        {
            if (file == null)
            {
                return Response.Fail<FileWriterOptions>("File was empty");
            }
            var options = new UploadFileOption();
            uploadFileOption.Invoke(options);
            var processors = _processorRepository.GetAllProcessors();
            var result = new FileWriterOptions()
            {
                File = file
            };
            foreach (var uploadOptionProcessor in processors)
            {
                uploadOptionProcessor.Process(options, ref result);
            }
            return Response.Success(result, "File writer options created");
        }

        public Response<FileWriterOptions> ProcessAll(IFormFile file, UploadFileOption uploadFileOption)
        {
            if (file == null)
            {
                return Response.Fail<FileWriterOptions>("File was empty");
            }
            var processors = _processorRepository.GetAllProcessors();
            var result = new FileWriterOptions()
            {
                File = file
            };
            foreach (var uploadOptionProcessor in processors)
            {
                uploadOptionProcessor.Process(uploadFileOption, ref result);
            }
            return Response.Success(result, "File writer options created");
        }
    }
}
