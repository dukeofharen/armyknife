using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Armyknife.Tests.Integration.Tools
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

         WebServiceMock
            .Setup(m => m.DoRequestAsync(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync(response);

         string[] args = GetArgs("tinyurl https://google.com");

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(tinyUrl, Output);
      }
   }
}
