using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class Base64DecodeToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task Base64DecodeTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("base64decode dGhpcyBpcyB0aGUgaW5wdXQ=");
         string expectedOutput = "this is the input";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
