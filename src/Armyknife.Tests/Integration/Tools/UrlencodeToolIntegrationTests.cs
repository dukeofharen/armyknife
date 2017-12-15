using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class UrlencodeToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task UrlencodeTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("urlencode https://google.com");
         string expectedOutput = "https%3a%2f%2fgoogle.com";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
