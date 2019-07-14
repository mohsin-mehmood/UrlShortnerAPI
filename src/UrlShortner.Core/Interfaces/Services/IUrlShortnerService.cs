using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortner.Core.Entities;

namespace UrlShortner.Core.Interfaces.Services
{
    public interface IUrlShortnerService
    {
        Task<ShortenedUrl> AddShortenedUrl(string url);

        Task<IEnumerable<ShortenedUrl>> SearchByUrlAsync(string url);

        Task<string> InflateShortenedUrl(string urlHash);
    }
}
