using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class WeeknumberToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task WeeknumberTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("weeknumber");
         var now = new DateTime(2017, 11, 12);
         string expectedOutput = "45";

         DateTimeServiceMock
            .Setup(m => m.Now)
            .Returns(now);

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
