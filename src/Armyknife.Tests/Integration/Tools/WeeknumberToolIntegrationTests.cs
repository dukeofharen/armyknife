using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class WeeknumberToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task WeeknumberTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs($"weeknumber");
         var now = new DateTime(2017, 11, 12);
         string expectedOutput = "45";

         _dateTimeServiceMock
            .Setup(m => m.Now)
            .Returns(now);

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, _output);
      }
   }
}
