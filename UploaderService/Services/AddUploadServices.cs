using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using UploaderService.Client;
using UploaderService.FileWriters;
using UploaderService.Helpers;
using UploaderService.Initializer;
using UploaderService.UploadOptionsProcessors;
using UploaderService.Validators;

namespace UploaderService.Services
{
    public static class AddUploadServices
    {
        public static IServiceCollection AddUploadService(this IServiceCollection services)
        {
            services.AddSingleton<IValidator, Validator>();
            services.AddSingleton<IFileWriter, FileWriter>();
            services.AddSingleton<IPathFinder, PathFinder>();
            services.AddSingleton<IPathHandler, PathHandler>();
            services.AddSingleton<IUploadHandler, UploadHandler>();
            services.AddSingleton<IFileNameHandler, FileNameHandler>();
            services.AddSingleton<IExtensionHandler, ExtensionHandler>();
            services.AddSingleton<IDirectoryHandler, DirectoryHandler>();
            services.AddSingleton<IProcessorHandler, ProcessorHandler>();
            services.AddSingleton<IUploadInitializer, UploadInitializer>();
            services.AddSingleton<IValidationHandler, ValidationHandler>();
            services.AddSingleton<IUploadInitializer, UploadInitializer>();
            services.AddSingleton<IProcessorRepository, ProcessorRepository>();
            services.AddSingleton<IFileExistenceHandler, FileExistenceHandler>();
            services.AddSingleton<IValidationRepository, ValidationRepository>();
                   
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));




            return services;
        }
    }
}