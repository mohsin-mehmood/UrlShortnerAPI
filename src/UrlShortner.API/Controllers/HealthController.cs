using Microsoft.AspNetCore.Mvc;

namespace UrlShortner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Ensures the service is up and running!
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok("All good!");
        }

    }
}