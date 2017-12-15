using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.Tests.Integration.Tools
{
   [TestClass]
   public class HtmlencodeToolIntegrationTests : IntegrationTestBase
   {
      [TestMethod]
      public async Task HtmlencodeTool_IntegrationTest()
      {
         // arrange
         var args = GetArgs("htmlencode <html></html>");
         string expectedOutput = "&lt;html&gt;&lt;/html&gt;";

         // act
         await Executor.ExecuteAsync(args);

         // assert
         Assert.AreEqual(expectedOutput, Output);
      }
   }
}
