﻿namespace UrlShortner.Core.Dto
{
    public class ShortenedUrlModel
    {
        public long UrlId { get; set; }

        public string Url { get; set; }

        public string UrlHash { get; set; }
    }
}
