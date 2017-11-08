using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
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
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedExtension, _output);
      }
   }
}
