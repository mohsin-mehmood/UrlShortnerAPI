using System.Collections.Generic;
using UrlShortner.Core.Interfaces.Providers;

namespace UrlShortner.Infrastructure.Providers
{
    public class UrlHashBase62Provider : IUrlHashProvider
    {
        private const string BASE_62_SET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public string GenerateHash(long n)
        {
            List<char> hashChars = new List<char>();

            // Convert given integer id to a base 62 number 
            while (n > 0)
            {
                // use above map to store actual character 
                // in short url                  

                hashChars.Add(BASE_62_SET[(int)n % 62]);

                n = n / 62;
            }

            hashChars.Reverse();

            return new string(hashChars.ToArray());
        }
    }
}
