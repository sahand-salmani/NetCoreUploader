using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace UploaderService.Validators
{
    public interface IValidationRepository
    {
        IUploadValidation[] GetAllValidations();
    }
    public class ValidationRepository : IValidationRepository
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IUploadValidation[] GetAllValidations()
        {
            var types = typeof(IUploadValidation).Assembly.GetTypes().Where(e =>
                typeof(IUploadValidation).IsAssignableFrom(e) && !e.IsAbstract && !e.IsInterface);

            return types.Select(type => ActivatorUtilities.CreateInstance(_serviceProvider, type) as IUploadValidation)
                .ToArray();

        }
    }
}
