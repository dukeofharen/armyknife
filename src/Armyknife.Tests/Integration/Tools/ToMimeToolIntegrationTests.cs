using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
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
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedExtension, _output);
      }
   }
}
