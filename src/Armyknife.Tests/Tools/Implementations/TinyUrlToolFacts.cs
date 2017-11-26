using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Resources;
using Armyknife.Services.Interfaces;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Armyknife.Tools.Tests.Implementations
{
    [TestClass]
    public class TinyUrlToolFacts
    {
        private Mock<IWebService> _webServiceMock;
        private TinyUrlTool _tool;

        [TestInitialize]
        public void Initialize()
        {
            _webServiceMock = new Mock<IWebService>();
            _tool = new TinyUrlTool(_webServiceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _webServiceMock.VerifyAll();
        }

        [TestMethod]
        public async Task TinyUrlTool_ExecuteAsync_InputNotSet_ShouldThrowArmyknifeException()
        {
            // arrange
            var args = new Dictionary<string, string>();

            // act
            var exception = await Assert.ThrowsExceptionAsync<ArmyknifeException>(() => _tool.ExecuteAsync(args));

            // assert
            Assert.AreEqual(ExceptionResources.NoInput, exception.Message);
        }

        [TestMethod]
        public async Task TinyUrlTool_ExecuteAsync_HttpStatusCodeUnexpected_ShouldThrowArmyknifeException()
        {
            // arrange
            var args = new Dictionary<string, string>
            {
                { Constants.InputKey, "https://google.com" }
            };
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            _webServiceMock
                .Setup(m => m.DoRequestAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(responseMessage);

            // act
            var exception = await Assert.ThrowsExceptionAsync<ArmyknifeException>(() => _tool.ExecuteAsync(args));

            // assert
            Assert.IsTrue(exception.Message.Contains(responseMessage.StatusCode.ToString()));
        }

        [TestMethod]
        public async Task TinyUrlTool_ExecuteAsync_HappyFlow()
        {
            // arrange
            HttpRequestMessage actualRequestMessage = null;
            string url = "https://google.com";
            string actualContent = "http://tinyurl.com/123";
            string expectedApiUrl = $"http://tinyurl.com/api-create.php?url={WebUtility.UrlEncode(url)}";
            var args = new Dictionary<string, string>
            {
                { Constants.InputKey, url }
            };
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(actualContent)
            };

            _webServiceMock
                .Setup(m => m.DoRequestAsync(It.IsAny<HttpRequestMessage>()))
                .Callback<HttpRequestMessage>(m => actualRequestMessage = m)
                .ReturnsAsync(responseMessage);

            // act
            string result = await _tool.ExecuteAsync(args);

            // assert
            Assert.AreEqual(actualContent, result);
            Assert.IsNotNull(actualRequestMessage);
            Assert.AreEqual(expectedApiUrl, actualRequestMessage.RequestUri.OriginalString);
        }
    }
}
