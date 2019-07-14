using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using UrlShortner.API.Controllers;
using UrlShortner.Core.Dto;
using UrlShortner.Core.Interfaces.Services;

namespace UrlShortner.API.Tests.Controllers
{
    public class UrlControllerTests
    {

        #region Inflate Action Tests
        [Test]
        public async Task Test_InflateAction_NullInput()
        {
            // Setup
            var urlShortnerServiceMock = new Mock<IUrlShortnerService>();
            var mapperMock = new Mock<IMapper>();


            //urlShortnerServiceMock.Setup(s => s.InflateShortenedUrl(It.IsAny<string>()))
            //    .Returns(Task.FromResult<string>(null));

            var urlController = new UrlController(urlShortnerServiceMock.Object, mapperMock.Object);

            var result = await urlController.Inflate("sds");

            // Assert
            Assert.IsTrue(result is NotFoundResult);
        }

        [Test]
        public async Task Test_InflateAction_InvalidHash()
        {
            // Setup
            var urlShortnerServiceMock = new Mock<IUrlShortnerService>();
            var mapperMock = new Mock<IMapper>();


            urlShortnerServiceMock.Setup(s => s.InflateShortenedUrl(It.IsAny<string>()))
                .Returns(Task.FromResult<string>(null));

            // Action
            var urlController = new UrlController(urlShortnerServiceMock.Object, mapperMock.Object);

            var result = await urlController.Inflate("sds");

            // Assert
            Assert.IsTrue(result is NotFoundResult);
        }

        [Test]
        public async Task Test_InflateAction_ValidHash()
        {
            // Setup
            const string originalUrl = "http://example.com";

            var urlShortnerServiceMock = new Mock<IUrlShortnerService>();
            var mapperMock = new Mock<IMapper>();


            urlShortnerServiceMock.Setup(s => s.InflateShortenedUrl(It.IsAny<string>()))
                .Returns(Task.FromResult<string>(originalUrl));

            // Action
            var urlController = new UrlController(urlShortnerServiceMock.Object, mapperMock.Object);

            var result = await urlController.Inflate("sds");

            // Assert
            Assert.IsTrue(result is OkObjectResult);
            Assert.AreEqual(((OkObjectResult)result).Value, originalUrl);
        }

        #endregion

        #region Shorten Action Tests
        [Test]
        public async Task Test_ShortenAction_NullInputUrl()
        {
            // Setup
            var urlShortnerServiceMock = new Mock<IUrlShortnerService>();
            var mapperMock = new Mock<IMapper>();

            // Action
            var urlController = new UrlController(urlShortnerServiceMock.Object, mapperMock.Object);

            urlController.ModelState.AddModelError("Url", "Url is missing");

            var result = await urlController.Shorten(new ShortenUrlRequest
            {
                Url = null
            });

            // Assert
            Assert.IsTrue(result is BadRequestObjectResult);
        }

        [Test]
        public async Task Test_ShortenAction_InvalidInputUrl()
        {
            // Setup
            var urlShortnerServiceMock = new Mock<IUrlShortnerService>();
            var mapperMock = new Mock<IMapper>();



            // Action
            var urlController = new UrlController(urlShortnerServiceMock.Object, mapperMock.Object);

            urlController.ModelState.AddModelError("Url", "Invalid url format");

            var result = await urlController.Shorten(new ShortenUrlRequest
            {
                Url = "httpsdsds"
            });

            // Assert
            Assert.IsTrue(result is BadRequestObjectResult);
        }

        [Test]
        public async Task Test_ShortenAction_ValidInputUrl()
        {
            // Setup
            var urlShortnerServiceMock = new Mock<IUrlShortnerService>();
            var mapperMock = new Mock<IMapper>();
            const string urlHash = "mJ";

            urlShortnerServiceMock.Setup(s => s.AddShortenedUrl(It.IsAny<string>()))
                .Returns(Task.FromResult(urlHash));

            // Action
            var urlController = new UrlController(urlShortnerServiceMock.Object, mapperMock.Object);


            var result = await urlController.Shorten(new ShortenUrlRequest
            {
                Url = "http://www.yahoo.com"
            });

            // Assert
            Assert.IsTrue(result is OkObjectResult);
            Assert.AreEqual(((OkObjectResult)result).Value, urlHash);
        }

        #endregion
    }
}
