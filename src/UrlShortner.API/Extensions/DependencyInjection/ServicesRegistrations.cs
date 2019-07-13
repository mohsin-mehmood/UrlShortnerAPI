using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UrlShortner.Core.Interfaces.Services;
using UrlShortner.Core.Services;

namespace UrlShortner.API.Extensions.DependencyInjection
{
    public static class ServicesRegistrations
    {
        public static IServiceCollection AddServicesRegistrations(this IServiceCollection services)
        {

            services.TryAddScoped<IUrlShortnerService, UrlShortnerService>();

            return services;
        }
    }
}
