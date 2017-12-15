using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class LongurlToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task LongurlTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("longurl http://tinyurl.com/123");
         string expectedOutput = "https://ducode.blog/";

         var response = new HttpResponseMessage
         {
            RequestMessage = new HttpRequestMessage
            {
               RequestUri = new Uri(expectedOutput)
            }
         };

         WebServiceMock
            .Setup(m => m.DoRequestAsync(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync(response);

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
