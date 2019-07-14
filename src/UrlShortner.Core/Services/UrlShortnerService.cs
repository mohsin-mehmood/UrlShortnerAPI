using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortner.Core.Entities;
using UrlShortner.Core.Interfaces.Providers;
using UrlShortner.Core.Interfaces.Repositories;
using UrlShortner.Core.Interfaces.Services;

namespace UrlShortner.Core.Services
{
    public class UrlShortnerService : IUrlShortnerService
    {
        private readonly IUrlShortnerRepository _urlShortnerRepository;
        private readonly IUniqueIdGeneratorProvider _uniqueIdGeneratorProvider;
        private readonly IUrlHashProvider _urlHashProvider;

        public UrlShortnerService(IUrlShortnerRepository urlShortnerRepository,
                IUniqueIdGeneratorProvider uniqueIdGeneratorProvider,
                IUrlHashProvider urlHashProvider)
        {
            _urlShortnerRepository = urlShortnerRepository;
            _uniqueIdGeneratorProvider = uniqueIdGeneratorProvider;
            _urlHashProvider = urlHashProvider;
        }


        public async Task<string> AddShortenedUrl(string url)
        {
            // Check for existing url
            var urlInfo = await _urlShortnerRepository.LookupByUrlAsync(url);

            if (urlInfo != null)
            {
                return urlInfo.UrlHash;
            }

            // Generate next UrlId
            var nextUrlId = await _uniqueIdGeneratorProvider.GenerateNextIdAsync();


            var urlEntity = new ShortenedUrl
            {
                Id = nextUrlId,
                Url = url,
                UrlHash = _urlHashProvider.GenerateHash(nextUrlId),
                CreatedDateTime = DateTime.Now.ToUniversalTime()
            };

            return await _urlShortnerRepository.AddShortenedUrlAsync(urlEntity);
        }

        public async Task<string> InflateShortenedUrl(string urlHash)
        {
            var urlEntity = await _urlShortnerRepository.LookupByHashAsync(urlHash);

            if (urlEntity != null)
            {
                return urlEntity.Url;
            }

            return null;
        }

        public async Task<IEnumerable<ShortenedUrl>> SearchByUrlAsync(string url)
        {
            return await _urlShortnerRepository.SearchByUrlAsync(url);
        }
    }
}
