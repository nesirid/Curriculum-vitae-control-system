using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Parsers;
using Service.Parsers.Interfaces;
using Service.Services;
using Service.Services.Interfaces;


namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            // Automatic mapping profiling
            //services.AddAutoMapper(typeof(MappingProfile).Assembly);

            // Add services to the DI container
            services.AddMemoryCache();

            // Registering file parsers
            services.AddScoped<IFileParser, DocxFileParser>();
            services.AddScoped<IFileParser, PdfFileParser>();

            // Basic services
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<ICandidatePhotoService, CandidatePhotoService>();

            services.AddScoped<IFileProcessingService, FileProcessingService>();

            services.AddScoped<IDocFileTypeRecognizer, DocFileTypeRecognizer>();
            services.AddScoped<IPhotoFileTypeRecognizer, PhotoFileTypeRecognizer>();





            // Registration of validators (if any)
            //services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
