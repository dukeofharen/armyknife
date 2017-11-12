using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class UrldecodeToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task UrldecodeTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs($"urldecode https%3a%2f%2fgoogle.com");
         string expectedOutput = "https://google.com";

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, _output);
      }
   }
}
