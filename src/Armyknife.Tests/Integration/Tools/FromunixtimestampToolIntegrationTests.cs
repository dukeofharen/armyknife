using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class FromunixtimestampToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task FromunixtimestampTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("fromunixtimestamp 1513889220");
         string expectedOutput = "2017-12-21 20:47:00:000";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
