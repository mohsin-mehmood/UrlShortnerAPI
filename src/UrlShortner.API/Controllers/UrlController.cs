using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortner.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public ActionResult Shorten([FromBody] string url)
        {
            return Ok(url);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult Inflate([FromBody] string url)
        {
            return Ok(url);
        }
    }
}