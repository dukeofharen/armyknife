using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class LongurlToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task LongurlTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs($"longurl http://tinyurl.com/123");
         string expectedOutput = "https://ducode.blog/";

         var response = new HttpResponseMessage
         {
            RequestMessage = new HttpRequestMessage
            {
               RequestUri = new Uri(expectedOutput)
            }
         };

         _webServiceMock
            .Setup(m => m.DoRequestAsync(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync(response);

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, _output);
      }
   }
}
