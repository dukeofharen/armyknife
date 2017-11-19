using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class JsonuglifyToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task JsonuglifyTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs($"jsonuglify {{\r\n  \"key\": \"value\"\r\n}}");
         string expectedOutput = @"{""key"":""value""}";

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, _output);
      }
   }
}
