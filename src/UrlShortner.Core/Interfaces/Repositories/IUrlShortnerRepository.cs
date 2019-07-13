using System.Threading.Tasks;
using UrlShortner.Core.Entities;

namespace UrlShortner.Core.Interfaces.Repositories
{
    public interface IUrlShortnerRepository
    {
        Task<string> AddShortenedUrlAsync(ShortenedUrl newShortenedUrl);

        Task<ShortenedUrl> LookupByUrlAsync(string url);

        Task<ShortenedUrl> LookupByHashAsync(string urlHash);
    }
}
