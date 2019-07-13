using Microsoft.Extensions.DependencyInjection;
using SnowMaker;
using System;
using System.Threading.Tasks;
using UrlShortner.Core.Interfaces.Providers;

namespace UrlShortner.Infrastructure.External
{
    public class UniqueIdGeneratorSnowMakerProvider : IUniqueIdGeneratorProvider
    {
        const string SCOPE_NAME = "UrlShortnerUrlIds";

        private readonly IServiceProvider _serviceProvider;
        public UniqueIdGeneratorSnowMakerProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<long> GenerateNextIdAsync()
        {
            var uniqueGenerator = _serviceProvider.GetService<IUniqueIdGenerator>();

            return await uniqueGenerator.NextIdAsync(SCOPE_NAME);
        }
    }
}
