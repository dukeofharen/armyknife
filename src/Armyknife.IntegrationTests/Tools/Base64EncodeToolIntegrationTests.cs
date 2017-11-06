using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.IntegrationTests.Tools
{
   [TestClass]
   public class Base64EncodeToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task Base64EncodeTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs($"base64encode this is the input");
         string expectedOutput = "dGhpcyBpcyB0aGUgaW5wdXQ=";

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, _output);
      }
   }
}
