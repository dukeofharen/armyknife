using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Armyknife.Exceptions;
using Armyknife.Models;
using Armyknife.Services.Interfaces;
using Armyknife.Tools.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Armyknife.Tests.Tools.Implementations
{
   [TestClass]
   public class LongurlToolFacts
   {
      private Mock<IWebService> _webServiceMock;
      private LongurlTool _tool;

      [TestInitialize]
      public void Initialize()
      {
         _webServiceMock = new Mock<IWebService>();
         _tool = new LongurlTool(_webServiceMock.Object);
      }

      [TestCleanup]
      public void Cleanup()
      {
         _webServiceMock.VerifyAll();
      }

      [TestMethod]
      public async Task LongurlTool_ExecuteAsync_NoInput_ShouldThrowArmyknifeException()
      {
         // arrange
         var argsDictionary = new Dictionary<string, string>();

         // act / assert
         await Assert.ThrowsExceptionAsync<ArmyknifeException>(() => _tool.ExecuteAsync(argsDictionary));
      }

      [TestMethod]
      public async Task LongurlTool_ExecuteAsync_HappyFlow()
      {
         // arrange
         HttpRequestMessage actualRequest = null;
         string input = "http://tinyurl.com/123";
         string expectedOutput = "https://ducode.blog/";
         var argsDictionary = new Dictionary<string, string>
         {
            { Constants.InputKey, input }
         };

         var response = new HttpResponseMessage
         {
            RequestMessage = new HttpRequestMessage
            {
               RequestUri = new Uri(expectedOutput)
            }
         };

         _webServiceMock
            .Setup(m => m.DoRequestAsync(It.IsAny<HttpRequestMessage>()))
            .Callback<HttpRequestMessage>(r => actualRequest = r)
            .ReturnsAsync(response);

         // act
         string output = await _tool.ExecuteAsync(argsDictionary);

         // assert
         Assert.AreEqual(expectedOutput, output);
         Assert.IsNotNull(actualRequest);
         Assert.AreEqual(input, actualRequest.RequestUri.OriginalString);
         Assert.AreEqual(Constants.UserAgent, actualRequest.Headers.GetValues("User-Agent").First());
      }
   }
}
