using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

        public UrlController(IUrlShortnerService urlShortnerService)
        {

            _urlShortnerService = urlShortnerService;
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
        /// <param name="shortUrl">Short Url Code/Hash</param>
        /// <returns>Full url</returns>
        [HttpGet]
        [Route("[action]/{shortUrl}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Inflate([FromBody] string shortUrl)
        {
            var originalUrl = await _urlShortnerService.InflateShortenedUrl(shortUrl);

            if (!string.IsNullOrWhiteSpace(originalUrl))
            {
                return Ok(originalUrl);
            }
            else
            {
                return NotFound();
            }
        }
    }
}