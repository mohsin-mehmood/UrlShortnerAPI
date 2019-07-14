using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortner.Core.Entities;

namespace UrlShortner.Core.Interfaces.Repositories
{
    public interface IUrlShortnerRepository
    {
        Task<ShortenedUrl> AddShortenedUrlAsync(ShortenedUrl newShortenedUrl);

        Task<ShortenedUrl> LookupByUrlAsync(string url);

        Task<ShortenedUrl> LookupByHashAsync(string urlHash);

        Task<IEnumerable<ShortenedUrl>> SearchByUrlAsync(string url);
    }
}
