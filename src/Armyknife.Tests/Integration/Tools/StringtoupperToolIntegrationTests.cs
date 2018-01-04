using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class StringtoupperToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task StringtoupperTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("stringtoupper some input");
         string expectedOutput = "SOME INPUT";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
