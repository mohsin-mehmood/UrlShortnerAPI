using System.ComponentModel.DataAnnotations;

namespace UrlShortner.Core.Dto
{
    public class ShortenUrlRequest
    {
        [Required, Url]
        public string Url { get; set; }
    }
}
