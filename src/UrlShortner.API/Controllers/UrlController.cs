using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortner.Core.Dto;
using UrlShortner.Core.Interfaces.Services;

namespace UrlShortner.API.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlShortnerService _urlShortnerService;
        private readonly IMapper _mapper;

        public UrlController(IUrlShortnerService urlShortnerService, IMapper mapper)
        {
            _urlShortnerService = urlShortnerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Converts full url into short url.
        /// </summary>
        /// <param name="url">Full url</param>
        /// <returns>Short url code/hash</returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Shorten([FromBody] string url)
        {
            return Ok(await _urlShortnerService.AddShortenedUrl(url));
        }

        /// <summary>
        /// Converts the shortUrl back to original full url
        /// </summary>
        /// <param name="urlHash">Url hash code/Hash</param>
        /// <returns>Full url</returns>
        [HttpGet]
        [Route("[action]/{urlHash}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Inflate(string urlHash)
        {
            var originalUrl = await _urlShortnerService.InflateShortenedUrl(urlHash);

            if (!string.IsNullOrWhiteSpace(originalUrl))
            {
                return Ok(originalUrl);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Search for already stored urls info
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SearchUrls([FromBody] string searchTerm)
        {
            var searchResults = await _urlShortnerService.SearchByUrlAsync(searchTerm);

            if (searchResults.Any())
            {
                return Ok(_mapper.Map<IEnumerable<ShortenedUrlModel>>(searchResults));
            }
            else
            {
                return NoContent();
            }
        }

    }
}