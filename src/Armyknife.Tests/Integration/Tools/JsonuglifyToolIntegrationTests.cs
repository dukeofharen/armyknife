using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class JsonuglifyToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task JsonuglifyTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("jsonuglify {\r\n  \"key\": \"value\"\r\n}");
         string expectedOutput = @"{""key"":""value""}";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
