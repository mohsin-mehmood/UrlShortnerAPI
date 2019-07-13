using System.Threading.Tasks;
using UrlShortner.Core.Dto;

namespace UrlShortner.Core.Interfaces.Services
{
    public interface IUrlShortnerService
    {
        Task<string> AddShortenedUrl(string url);

        Task<ShortenedUrlResponse> LookupByUrl(string url);

        Task<string> InflateShortenedUrl(string urlHash);
    }
}
