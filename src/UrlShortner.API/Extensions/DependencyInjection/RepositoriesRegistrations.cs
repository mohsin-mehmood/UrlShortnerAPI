using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UrlShortner.Core.Interfaces.Repositories;
using UrlShortner.Infrastructure.Data.Repositories;

namespace UrlShortner.API.Extensions.DependencyInjection
{
    public static class RepositoriesRegistrations
    {

        public static IServiceCollection AddRepositoriesRegistrations(this IServiceCollection services)
        {

            services.TryAddScoped<IUrlShortnerRepository, UrlShortnerRepository>();

            return services;
        }
    }
}
