using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortner.Core.Entities
{
    public class ShortenedUrl
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [MaxLength(2500)]
        public string Url { get; set; }

        [MaxLength(10)]
        public string UrlHash { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
