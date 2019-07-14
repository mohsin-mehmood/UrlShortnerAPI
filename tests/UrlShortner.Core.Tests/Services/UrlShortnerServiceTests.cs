using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using UrlShortner.Core.Entities;
using UrlShortner.Core.Interfaces.Providers;
using UrlShortner.Core.Interfaces.Repositories;
using UrlShortner.Core.Services;

namespace UrlShortner.Core.Tests.Services
{
    public class UrlShortnerServiceTests
    {
        [Test]
        public async Task Test_AddShortenedUrl_AlreadyExistingUrl()
        {
            var alreadyAddedUrlHash = "dF";
            var inputUrl = "http://www.example.com";

            var repoMock = new Mock<IUrlShortnerRepository>();
            repoMock.Setup(r => r.LookupByUrlAsync(It.IsAny<string>())).Returns(Task.FromResult(
                new ShortenedUrl
                {
                    Id = 1,
                    Url = inputUrl,
                    UrlHash = alreadyAddedUrlHash
                })
            );

            var urlShortnerService = new UrlShortnerService(repoMock.Object, null, null);

            var result = await urlShortnerService.AddShortenedUrl(inputUrl);

            Assert.AreEqual(result.UrlHash, alreadyAddedUrlHash);
            Assert.AreEqual(result.Url, inputUrl);
        }

        [Test]
        public async Task Test_AddShortenedUrl_NewUrl()
        {
            // Setup
            var urlHash = "dF";
            var inputUrl = "http://www.example.com";

            var randomId = (long)new Random(111).Next();

            var repoMock = new Mock<IUrlShortnerRepository>();
            repoMock.Setup(r => r.LookupByUrlAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<ShortenedUrl>(null));

            repoMock.Setup(r => r.AddShortenedUrlAsync(It.IsAny<ShortenedUrl>())).Returns(
                Task.FromResult<ShortenedUrl>(new ShortenedUrl
                {
                    Id = randomId,
                    Url = inputUrl,
                    UrlHash = urlHash
                }));

            var uniqueIdGeneratorMock = new Mock<IUniqueIdGeneratorProvider>();
            uniqueIdGeneratorMock.Setup(u => u.GenerateNextIdAsync())
                .Returns(Task.FromResult(randomId));

            var urlHashProviderMock = new Mock<IUrlHashProvider>();
            urlHashProviderMock.Setup(h => h.GenerateHash(It.IsAny<long>()))
                .Returns(urlHash);

            // Action

            var urlShortnerService = new UrlShortnerService(repoMock.Object, uniqueIdGeneratorMock.Object, urlHashProviderMock.Object);

            var result = await urlShortnerService.AddShortenedUrl(inputUrl);

            // Assert
            Assert.AreEqual(result.UrlHash, urlHash);
            Assert.AreEqual(result.Id, randomId);
            Assert.AreEqual(result.Url, inputUrl);
        }
    }
}
