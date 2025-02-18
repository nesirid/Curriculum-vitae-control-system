using Microsoft.Extensions.DependencyInjection;
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

            // Registering file parsers

            // Basic services
            services.AddScoped<ICandidateService, CandidateService>();

            // Registration of validators (if any)
            //services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
