using Microsoft.Extensions.DependencyInjection;
using MoviesRental.Application.Contracts;
using MoviesRental.Infraestructure.Context;
using MoviesRental.Infraestructure.Repositories;

namespace MoviesRental.Infraestructure
{
    public static class InfraestructureServiceCollection
    {
        public static IServiceCollection AddWriteInfraestructure(this IServiceCollection services)
        {
            services.AddScoped<MoviesRentalWriteContext>();
            services.AddScoped<IDvdWriteRepository, DvdsWriteRepository>();
            services.AddScoped<IDirectorsWriteRepository, DirectorsWriteRepository>();
            return services;
        }
    }
}
