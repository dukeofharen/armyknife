using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class ToMimeToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task ToMimeTool_IntegrationTest()
      {
         // arrange
         string[] args = GetArgs("tomime pdf");
         string expectedExtension = "application/pdf";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedExtension, Output);
      }
   }
}
