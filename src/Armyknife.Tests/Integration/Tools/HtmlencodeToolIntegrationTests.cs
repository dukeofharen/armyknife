using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Armyknife.Integration.Tests.Tools
{
   [TestClass]
   public class HtmlencodeToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task HtmlencodeTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs($"htmlencode <html></html>");
         string expectedOutput = "&lt;html&gt;&lt;/html&gt;";

         // act
         await _executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, _output);
      }
   }
}
