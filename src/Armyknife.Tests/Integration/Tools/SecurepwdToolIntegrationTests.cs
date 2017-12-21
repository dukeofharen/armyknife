using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class SecurepwdToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task SecurepwdTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("securepwd");

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(20, Output.Length);
      }
   }
}
