using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class Base64EncodeToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task Base64EncodeTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("base64encode this is the input");
         string expectedOutput = "dGhpcyBpcyB0aGUgaW5wdXQ=";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
