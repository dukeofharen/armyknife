using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class FromMimeToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task FromMimeTool_IntegrationTest()
      {
         // arrange
         string[] args = GetArgs("frommime application/pdf");
         string expectedExtension = "pdf";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedExtension, Output);
      }
   }
}
