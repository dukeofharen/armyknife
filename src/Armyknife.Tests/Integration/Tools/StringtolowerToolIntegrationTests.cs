using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class StringtolowerToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task StringtolowerTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("stringtolower SOME INPUT");
         string expectedOutput = "some input";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
