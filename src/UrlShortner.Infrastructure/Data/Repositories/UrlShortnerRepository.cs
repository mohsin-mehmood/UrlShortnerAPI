using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortner.Core.Entities;
using UrlShortner.Core.Interfaces.Repositories;

namespace UrlShortner.Infrastructure.Data.Repositories
{
    public class UrlShortnerRepository : IUrlShortnerRepository
    {
        private readonly AppDBContext _dbContext;
        public UrlShortnerRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> AddShortenedUrlAsync(ShortenedUrl newShortenedUrl)
        {
            _dbContext.ShortenedUrls.Add(newShortenedUrl);
            await _dbContext.SaveChangesAsync();

            return newShortenedUrl.UrlHash;
        }

        public async Task<ShortenedUrl> LookupByHashAsync(string urlHash)
        {
            var urlInfo = await _dbContext.ShortenedUrls.FirstOrDefaultAsync(u => u.UrlHash.Equals(urlHash, StringComparison.InvariantCultureIgnoreCase));

            if (urlInfo != null)
            {
                return new ShortenedUrl
                {
                    Id = urlInfo.Id,
                    Url = urlInfo.Url,
                    UrlHash = urlInfo.UrlHash,
                    CreatedDateTime = urlInfo.CreatedDateTime
                };
            }

            return null;

        }

        public async Task<ShortenedUrl> LookupByUrlAsync(string url)
        {
            var urlInfo = await _dbContext.ShortenedUrls.FirstOrDefaultAsync(u => u.Url.Equals(url, StringComparison.InvariantCultureIgnoreCase));

            if (urlInfo != null)
            {
                return new ShortenedUrl
                {
                    Id = urlInfo.Id,
                    Url = urlInfo.Url,
                    UrlHash = urlInfo.UrlHash,
                    CreatedDateTime = urlInfo.CreatedDateTime
                };
            }

            return null;
        }

        public async Task<IEnumerable<ShortenedUrl>> SearchByUrlAsync(string url)
        {
            return await _dbContext.ShortenedUrls.Where(u => u.Url.Contains(url)).ToListAsync();
        }
    }
}
