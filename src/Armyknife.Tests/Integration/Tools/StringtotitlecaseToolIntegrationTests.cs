using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class StringtotitlecaseToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task StringtotitlecaseTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("stringtotitlecase some input");
         string expectedOutput = "Some Input";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
