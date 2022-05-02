using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace UploaderService.UploadOptionsProcessors
{
    public interface IProcessorRepository
    {
        IUploadOptionProcessor[] GetAllProcessors();
    }
    public class ProcessorRepository : IProcessorRepository
    {
        private readonly IServiceProvider _serviceProvider;

        public ProcessorRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IUploadOptionProcessor[] GetAllProcessors()
        {
            var types = typeof(IUploadOptionProcessor).Assembly.GetTypes().Where(e =>
                !e.IsAbstract && !e.IsInterface && typeof(IUploadOptionProcessor).IsAssignableFrom(e));

            return types
                .Select(e => ActivatorUtilities.CreateInstance(_serviceProvider, e) as IUploadOptionProcessor)
                .Where(e => e != null && e.Order != 0)
                .OrderBy(e => e.Order)
                .ToArray();

        }
    }
}
