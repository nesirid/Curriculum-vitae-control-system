using Microsoft.Extensions.DependencyInjection;


namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            // Automatic mapping profiling
            //services.AddAutoMapper(typeof(MappingProfile).Assembly);


            // Registration of validators (if any)
            //services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
