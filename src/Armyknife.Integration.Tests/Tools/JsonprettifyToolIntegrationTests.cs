using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class JsonprettifyToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task JsonprettifyTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs($"jsonprettify some input");
         string expectedOutput = string.Empty;

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, _output);
      }
   }
}