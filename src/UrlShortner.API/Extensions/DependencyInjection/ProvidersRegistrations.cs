using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UrlShortner.Core.Interfaces.Providers;
using UrlShortner.Infrastructure.External;
using UrlShortner.Infrastructure.Providers;

namespace UrlShortner.API.Extensions.DependencyInjection
{
    public static class ProvidersRegistrations
    {
        public static IServiceCollection AddProvidersRegistrations(this IServiceCollection services)
        {

            services.TryAddSingleton<IUniqueIdGeneratorProvider, UniqueIdGeneratorSnowMakerProvider>();
            services.TryAddSingleton<IUrlHashProvider, UrlHashBase62Provider>();
            return services;
        }
    }
}
