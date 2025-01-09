using MoviesRental.Application;
using MoviesRental.Infraestructure;
using MoviesRental.Query.Application;
using MoviesRental.Query.Infraestructure;
using MoviesRental.WebAPI.Cache;

namespace MoviesRental.WebAPI.Setup
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddWriteApplication();
            services.AddWriteInfraestructure();
            services.AddReadApplication();
            services.AddReadInfraestructure();
            services.AddScoped<ICacheRepository, CacheRepository>();
            //services.AddScoped<IMediatorHandler, MediatorHandler>();
            return services;
        }
    }
}
