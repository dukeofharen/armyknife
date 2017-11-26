using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class TinyUrlToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task TinyUrlTool_IntegrationTest()
      {
         // arrange
         string tinyUrl = "http://tinyurl.com/321";
         var response = new HttpResponseMessage
         {
            Content = new StringContent(tinyUrl)
         };

         _webServiceMock
            .Setup(m => m.DoRequestAsync(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync(response);

         string[] args = GetArgs("tinyurl https://google.com");

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(tinyUrl, _output);
      }
   }
}
