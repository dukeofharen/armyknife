using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class Base64DecodeToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task Base64DecodeTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs($"base64decode dGhpcyBpcyB0aGUgaW5wdXQ=");
         string expectedOutput = "this is the input";

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, _output);
      }
   }
}
