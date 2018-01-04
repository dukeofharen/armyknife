using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class WordcountToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task WordcountTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("wordcount some input");
         string expectedOutput = "2";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
