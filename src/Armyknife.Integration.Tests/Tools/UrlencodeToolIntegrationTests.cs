using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class UrlencodeToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task UrlencodeTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs($"urlencode https://google.com");
         string expectedOutput = "https%3a%2f%2fgoogle.com";

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, _output);
      }
   }
}
