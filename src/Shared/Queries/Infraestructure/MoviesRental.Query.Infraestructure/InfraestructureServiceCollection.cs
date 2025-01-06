using Microsoft.Extensions.DependencyInjection;
using MoviesRental.Query.Application.Contracts;
using MoviesRental.Query.Infraestructure.Context;
using MoviesRental.Query.Infraestructure.Repositories;

namespace MoviesRental.Query.Infraestructure
{
    public static class InfraestructureServiceCollection
    {
        public static IServiceCollection AddWriteInfraestructure(this IServiceCollection services)
        {
            services.AddScoped<IMoviesRentalReadContext, MoviesRentalReadContext>();
            services.AddScoped<IDvdQueryRepository, DvdsQueryRepository>();
            services.AddScoped<IDirectorQueryRepository, DirectorsQueryRepository>();
            return services;
        }
    }
}
