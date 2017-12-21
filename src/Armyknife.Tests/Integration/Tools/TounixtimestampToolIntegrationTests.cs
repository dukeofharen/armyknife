using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class TounixtimestampToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task TounixtimestampTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("tounixtimestamp 2017-08-20T15:00:00Z");
         string expectedOutput = "1503241200000";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
