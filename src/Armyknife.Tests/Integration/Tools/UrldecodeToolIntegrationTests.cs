using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class UrldecodeToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task UrldecodeTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("urldecode https%3a%2f%2fgoogle.com");
         string expectedOutput = "https://google.com";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
