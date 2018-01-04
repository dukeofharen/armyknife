using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class StringlengthToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task StringlengthTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("stringlength some input");
         string expectedOutput = "10";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
